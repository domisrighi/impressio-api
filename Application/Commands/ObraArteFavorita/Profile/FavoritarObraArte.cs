using AutoMapper;
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
        CreateMap<PaginacaoResposta<ObterObraArteResultadoDTO>, PaginacaoResposta<ObterObraArteRespostaDTO>>();
    }
}