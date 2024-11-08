using AutoMapper;
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
        // CreateMap<EditarObraArteCommand, ObraArteModel>().ReverseMap();
        // CreateMap<ObterObraArteResultadoDTO, ObraArteModel>()
        //     .ForMember(dest => dest.NomeUsuario, opt => opt.MapFrom(src => src.NomeUsuario))
        //     .ForMember(dest => dest.EmailUsuario, opt => opt.MapFrom(src => src.EmailUsuario))
        //     .ForMember(dest => dest.Apelido, opt => opt.MapFrom(src => src.Apelido))
        //     .ForMember(dest => dest.BiografiaUsuario, opt => opt.MapFrom(src => src.BiografiaUsuario))
        //     .ForMember(dest => dest.ImagemUsuario, opt => opt.MapFrom(src => src.ImagemUsuario))
        //     .ForMember(dest => dest.Publico, opt => opt.MapFrom(src => src.Publico));
        // CreateMap<ExcluirObraArteCommand, ObraArteModel>().ReverseMap();
        CreateMap<ObterObraArteCommand, ObterObraArteParametrosDTO>().ReverseMap();
        CreateMap<ObterObraArteResultadoDTO, ObterObraArteRespostaDTO>().ReverseMap();
        CreateMap<PaginacaoResposta<ObterObraArteResultadoDTO>, PaginacaoResposta<ObterObraArteRespostaDTO>>();
    }
}