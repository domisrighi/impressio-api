using AutoMapper;
using ImpressioApi_.Domain.DTO.Queries;
using ImpressioApi_.Domain.DTO.Read;
using ImpressioApi_.Domain.Interfaces.Queries;
using MediatR;

namespace ImpressioApi_.Application.Commands.ObraArte.Read
{
    public class ObterObraArteByIdHandler : IRequestHandler<ObterObraArteByIdCommand, CommandResult<ObterObraArteRespostaDTO>>
    {
        private readonly IObterObraArteQuery _obterObraArteQuery;
        private readonly IMapper _mapper;
        private ObterObraArteByIdCommand _request = null!;
        private CancellationToken _cancellationToken;
        private CommandResult<ObterObraArteRespostaDTO> _result = null!;

        public ObterObraArteByIdHandler(IObterObraArteQuery obterObraArteQuery, IMapper mapper)
        {
            _obterObraArteQuery = obterObraArteQuery ?? throw new ArgumentNullException(nameof(obterObraArteQuery));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<CommandResult<ObterObraArteRespostaDTO>> Handle(ObterObraArteByIdCommand request, CancellationToken cancellationToken)
        {
            _request = request;
            _cancellationToken = cancellationToken;
            _result = new CommandResult<ObterObraArteRespostaDTO>();

            var parametros = _mapper.Map<ObterObraArteByIdParametrosDTO>(_request);

            var resultado = _mapper.Map<ObterObraArteRespostaDTO>(await _obterObraArteQuery.ObterObraDeArteById(parametros.IdObraArte));

            return _result.Sucesso(resultado);
        }
    }
}
