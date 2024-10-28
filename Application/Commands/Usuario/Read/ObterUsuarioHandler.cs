using AutoMapper;
using ImpressioApi_.Domain.DTO.Queries;
using ImpressioApi_.Domain.DTO.Read;
using ImpressioApi_.Domain.Interfaces.Queries;
using ImpressioApi_.Domain.Queries;
using MediatR;

namespace ImpressioApi_.Application.Commands.Usuario.Read;

public class ObterUsuarioHandler: IRequestHandler<ObterUsuarioCommand, CommandResult<PaginacaoResposta<ObterUsuarioRespostaDTO>>>
{
    private readonly IObterUsuarioQuery _obterUsuarioQuery;
    private readonly IMapper _mapper;
    private ObterUsuarioCommand _request = null!;
    private CancellationToken _cancellationToken;
    private CommandResult<PaginacaoResposta<ObterUsuarioRespostaDTO>> _result = null!;

    public ObterUsuarioHandler(IObterUsuarioQuery obterUsuarioQuery, IMapper mapper)
    {
        _obterUsuarioQuery = obterUsuarioQuery ?? throw new ArgumentNullException(nameof(obterUsuarioQuery));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<CommandResult<PaginacaoResposta<ObterUsuarioRespostaDTO>>> Handle(ObterUsuarioCommand request, CancellationToken cancellationToken)
    {
        _request = request;
        var valida = await _request.Valida();
        _cancellationToken = cancellationToken;
        _result = new CommandResult<PaginacaoResposta<ObterUsuarioRespostaDTO>>();
        
        if (!valida)
        {
            return _result.AdicionarErros(_request.ObterErros());
        }

        var parametros = _mapper.Map<ObterUsuarioParametrosDTO>(_request);
        var resultado = _mapper.Map<PaginacaoResposta<ObterUsuarioRespostaDTO>>(await _obterUsuarioQuery.ObterUsuario(parametros));

        return _result.Sucesso(resultado);
    }
}