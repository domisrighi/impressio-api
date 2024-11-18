using AutoMapper;
using ImpressioApi_.Domain.DTO.Queries;
using ImpressioApi_.Domain.DTO.Read;
using ImpressioApi_.Domain.Model;
using ImpressioApi_.Domain.Queries;

namespace ImpressioApi_.Application.Commands.RegistrarVoto.Profile;

public class RegistrarVotoProfile: AutoMapper.Profile
{
    public RegistrarVotoProfile()
    {
        CreateMap<RegistrarVotoCommand, ObraVotoModel>().ReverseMap();
    }
}