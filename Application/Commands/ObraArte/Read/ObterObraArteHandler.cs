using AutoMapper;
using ImpressioApi_.Application.Commands.Usuario.Read;
using ImpressioApi_.Domain.DTO.Queries;
using ImpressioApi_.Domain.DTO.Read;
using ImpressioApi_.Domain.Interfaces.Queries;
using ImpressioApi_.Domain.Queries;
using MediatR;

namespace ImpressioApi_.Application.Commands.ObraArte.Read;

public class ObterObraArteHandler: IRequestHandler<ObterObraArteCommand, CommandResult<PaginacaoResposta<ObterObraArteRespostaDTO>>>
{
    private readonly IObterUsuarioQuery _obterUsuarioQuery;
    private readonly IObterObraArteQuery _obterObraArteQuery;
    private readonly IMapper _mapper;
    private ObterObraArteCommand _request = null!;
    private CancellationToken _cancellationToken;
    private CommandResult<PaginacaoResposta<ObterObraArteRespostaDTO>> _result = null!;

    public ObterObraArteHandler(IObterUsuarioQuery obterUsuarioQuery, IObterObraArteQuery obterObraArteQuery, IMapper mapper)
    {
        _obterUsuarioQuery = obterUsuarioQuery ?? throw new ArgumentNullException(nameof(obterUsuarioQuery));
        _obterObraArteQuery = obterObraArteQuery ?? throw new ArgumentNullException(nameof(obterObraArteQuery));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<CommandResult<PaginacaoResposta<ObterObraArteRespostaDTO>>> Handle(ObterObraArteCommand request, CancellationToken cancellationToken)
    {
        _request = request;
        var valida = await _request.Valida();
        _cancellationToken = cancellationToken;
        _result = new CommandResult<PaginacaoResposta<ObterObraArteRespostaDTO>>();
        
        if (!valida)
        {
            return _result.AdicionarErros(_request.ObterErros());
        }

        var parametros = _mapper.Map<ObterObraArteParametrosDTO>(_request);
        var resultado = _mapper.Map<PaginacaoResposta<ObterObraArteRespostaDTO>>(await _obterObraArteQuery.ObterObraDeArte(parametros));

        return _result.Sucesso(resultado);
    }
}