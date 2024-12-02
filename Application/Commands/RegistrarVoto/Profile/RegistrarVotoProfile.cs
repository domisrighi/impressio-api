using ImpressioApi_.Application.Commands.RegistrarVoto.Write;
using ImpressioApi_.Domain.Model;

namespace ImpressioApi_.Application.Commands.RegistrarVoto.Profile;

public class RegistrarVotoProfile: AutoMapper.Profile
{
    public RegistrarVotoProfile()
    {
        CreateMap<RegistrarVotoCommand, ObraVotoModel>().ReverseMap();
        CreateMap<ExcluirRegistroVotoCommand, ObraFavoritadaModel>().ReverseMap();
    }
}