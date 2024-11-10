using AutoMapper;
using ImpressioApi_.Domain.DTO.Queries;
using ImpressioApi_.Domain.DTO.Read;
using ImpressioApi_.Domain.Interfaces.Queries;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ImpressioApi_.Application.Commands.Usuario.Read
{
    public class ObterUsuarioByIdHandler : IRequestHandler<ObterUsuarioByIdCommand, CommandResult<ObterUsuarioRespostaDTO>>
    {
        private readonly IObterUsuarioQuery _obterUsuarioQuery;
        private readonly IMapper _mapper;
        private ObterUsuarioByIdCommand _request = null!;
        private CancellationToken _cancellationToken;
        private CommandResult<ObterUsuarioRespostaDTO> _result = null!;

        public ObterUsuarioByIdHandler(IObterUsuarioQuery obterUsuarioQuery, IMapper mapper)
        {
            _obterUsuarioQuery = obterUsuarioQuery ?? throw new ArgumentNullException(nameof(obterUsuarioQuery));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<CommandResult<ObterUsuarioRespostaDTO>> Handle(ObterUsuarioByIdCommand request, CancellationToken cancellationToken)
        {
            _request = request;
            _cancellationToken = cancellationToken;
            _result = new CommandResult<ObterUsuarioRespostaDTO>();

            var parametros = _mapper.Map<ObterUsuarioByIdParametrosDTO>(_request);

            var resultado = _mapper.Map<ObterUsuarioRespostaDTO>(await _obterUsuarioQuery.ObterUsuarioById(parametros.IdUsuario));

            return _result.Sucesso(resultado);
        }
    }
}
