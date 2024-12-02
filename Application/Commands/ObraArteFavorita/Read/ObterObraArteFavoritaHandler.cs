using AutoMapper;
using ImpressioApi_.Domain.DTO.Queries;
using ImpressioApi_.Domain.DTO.Read;
using ImpressioApi_.Domain.Interfaces.Queries;
using ImpressioApi_.Domain.Queries;
using MediatR;

namespace ImpressioApi_.Application.Commands.ObraArteFavorita.Read;

public class ObterObraArteFavoritaHandler: IRequestHandler<ObterObraArteFavoritaCommand, CommandResult<PaginacaoResposta<ObterObraArteFavoritaRespostaDTO>>>
{
    private readonly IObterObraArteFavoritaQuery _obterObraArteFavoritaQuery;
    private readonly IMapper _mapper;
    private ObterObraArteFavoritaCommand _request = null!;
    private CancellationToken _cancellationToken;
    private CommandResult<PaginacaoResposta<ObterObraArteFavoritaRespostaDTO>> _result = null!;

    public ObterObraArteFavoritaHandler(IObterObraArteFavoritaQuery obterObraArteFavoritaQuery, IMapper mapper)
    {
        _obterObraArteFavoritaQuery = obterObraArteFavoritaQuery ?? throw new ArgumentNullException(nameof(obterObraArteFavoritaQuery));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<CommandResult<PaginacaoResposta<ObterObraArteFavoritaRespostaDTO>>> Handle(ObterObraArteFavoritaCommand request, CancellationToken cancellationToken)
    {
        _request = request;
        var valida = await _request.Valida();
        _cancellationToken = cancellationToken;
        _result = new CommandResult<PaginacaoResposta<ObterObraArteFavoritaRespostaDTO>>();
        
        if (!valida)
        {
            return _result.AdicionarErros(_request.ObterErros());
        }

        var parametros = _mapper.Map<ObterObraArteFavoritaParametrosDTO>(_request);
        var resultado = _mapper.Map<PaginacaoResposta<ObterObraArteFavoritaRespostaDTO>>(await _obterObraArteFavoritaQuery.ObterObrasDeArteFavoritasByUsuario(parametros));

        return _result.Sucesso(resultado);
    }
}