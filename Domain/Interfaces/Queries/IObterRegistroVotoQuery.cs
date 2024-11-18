using ImpressioApi_.Domain.DTO.Queries;
using ImpressioApi_.Domain.DTO.Read;
using ImpressioApi_.Domain.Queries;

namespace ImpressioApi_.Domain.Interfaces.Queries
{
    public interface IObterRegistroVotoQuery
    {
        Task<ObterRegistroVotoResultadoDTO?> ObterRegistroVotoByObraArteEUsuario(int idObraArte, int idUsuario);
    }
}
