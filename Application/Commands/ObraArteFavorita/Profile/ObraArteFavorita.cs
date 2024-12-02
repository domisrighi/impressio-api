using ImpressioApi_.Application.Commands.ObraArteFavorita.Read;
using ImpressioApi_.Application.Commands.ObraArteFavorita.Write;
using ImpressioApi_.Domain.DTO.Queries;
using ImpressioApi_.Domain.DTO.Read;
using ImpressioApi_.Domain.Model;
using ImpressioApi_.Domain.Queries;

namespace ImpressioApi_.Application.Commands.ObraArteFavorita.Profile;

public class ObraArteFavoritaProfile: AutoMapper.Profile
{
    public ObraArteFavoritaProfile()
    {
        CreateMap<AdicionarObraArteFavoritaCommand, ObraFavoritadaModel>().ReverseMap();
        CreateMap<ExcluirObraArteFavoritaCommand, ObraFavoritadaModel>().ReverseMap();
        CreateMap<ObterObraArteFavoritaCommand, ObterObraArteFavoritaParametrosDTO>().ReverseMap();
        CreateMap<ObterObraArteFavoritaByIdCommand, ObterObraArteFavoritaByIdParametrosDTO>().ReverseMap();
        CreateMap<ObterObraArteFavoritaResultadoDTO, ObterObraArteFavoritaRespostaDTO>().ReverseMap();
        CreateMap<PaginacaoResposta<ObterObraArteFavoritaResultadoDTO>, PaginacaoResposta<ObterObraArteFavoritaRespostaDTO>>();
    }
}