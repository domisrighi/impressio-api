using ImpressioApi_.Domain.DTO.Queries;
using ImpressioApi_.Domain.Queries;

namespace ImpressioApi_.Domain.Interfaces.Queries;

public interface IObterObraArteQuery
{
    Task<PaginacaoResposta<ObterObraArteResultadoDTO>> ObterObraDeArte(ObterObraArteParametrosDTO parametros);
    Task<ObterObraArteResultadoDTO?> ObterObraDeArteById(int idObraArte);
}