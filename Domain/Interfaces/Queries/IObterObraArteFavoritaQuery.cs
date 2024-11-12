using ImpressioApi_.Domain.DTO.Queries;

namespace ImpressioApi_.Domain.Interfaces.Queries
{
    public interface IObterObraArteFavoritaQuery
    {
        Task<ObterObraArteFavoritaResultadoDTO?> ObterObraArteFavoritaByUsuarioEObraArte(int idObraArte, int idUsuario);
    }
}
