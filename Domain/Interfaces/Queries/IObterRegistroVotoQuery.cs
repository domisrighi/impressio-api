using ImpressioApi_.Domain.DTO.Queries;

namespace ImpressioApi_.Domain.Interfaces.Queries
{
    public interface IObterRegistroVotoQuery
    {
        Task<ObterRegistroVotoResultadoDTO?> ObterRegistroVotoByObraArteEUsuario(int idObraArte, int idUsuario);
    }
}
