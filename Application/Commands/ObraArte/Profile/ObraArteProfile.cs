using AutoMapper;
using ImpressioApi_.Application.Commands.ObraArte.Read;
using ImpressioApi_.Application.Commands.ObraArte.Write;
using ImpressioApi_.Application.Commands.Usuario.Read;
using ImpressioApi_.Domain.DTO.Queries;
using ImpressioApi_.Domain.DTO.Read;
using ImpressioApi_.Domain.Model;
using ImpressioApi_.Domain.Queries;

namespace ImpressioApi_.Application.Commands.ObraArte.Profile;

public class ObraArteProfile: AutoMapper.Profile
{
    public ObraArteProfile()
    {
        CreateMap<CadastrarObraArteCommand, ObraArteModel>().ReverseMap();
        CreateMap<EditarObraArteCommand, ObraArteModel>().ReverseMap();
        CreateMap<ExcluirObraArteCommand, ObraArteModel>().ReverseMap();
        CreateMap<ObterObraArteCommand, ObterObraArteParametrosDTO>().ReverseMap();
        CreateMap<ObterObraArteByIdCommand, ObterObraArteByIdParametrosDTO>().ReverseMap();
        CreateMap<ObterObraArteResultadoDTO, ObterObraArteRespostaDTO>().ReverseMap();
        CreateMap<PaginacaoResposta<ObterObraArteResultadoDTO>, PaginacaoResposta<ObterObraArteRespostaDTO>>();
    }
}