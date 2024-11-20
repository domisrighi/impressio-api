using ImpressioApi_.Domain.DTO.Queries;
using ImpressioApi_.Domain.DTO.Read;
using ImpressioApi_.Domain.Queries;

namespace ImpressioApi_.Domain.Interfaces.Queries
{
    public interface IObterObraArteFavoritaQuery
    {
        Task<ObterObraArteFavoritaResultadoDTO?> ObterObraArteFavoritaByUsuarioEObraArte(int idObraArte, int idUsuario);
        Task<PaginacaoResposta<ObterObraArteFavoritaResultadoDTO>> ObterObrasDeArteFavoritasByUsuario(ObterObraArteFavoritaParametrosDTO parametros);
        Task<PaginacaoResposta<ObterObraArteFavoritaResultadoDTO>> ObterObrasDeArteFavoritasByObraFavoritada(ObterObraArteFavoritaByIdParametrosDTO parametros);
    }
}
