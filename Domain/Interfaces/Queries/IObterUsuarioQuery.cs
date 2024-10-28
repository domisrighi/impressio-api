using ImpressioApi_.Domain.DTO.Queries;
using ImpressioApi_.Domain.Queries;

namespace ImpressioApi_.Domain.Interfaces.Queries;

public interface IObterUsuarioQuery
{
    Task<PaginacaoResposta<ObterUsuarioResultadoDTO>> ObterUsuario(ObterUsuarioParametrosDTO parametros);
}