using ImpressioApi_.Application.Commands.Usuario.Read;
using ImpressioApi_.Application.Commands.Usuario.Write;
using ImpressioApi_.Domain.DTO.Queries;
using ImpressioApi_.Domain.DTO.Read;
using ImpressioApi_.Domain.Model;
using ImpressioApi_.Domain.Queries;

namespace ImpressioApi_.Application.Commands.Usuario.Profile;

public class UsuarioProfile: AutoMapper.Profile
{
    public UsuarioProfile()
    {
        CreateMap<CadastrarUsuarioCommand, UsuarioModel>().ReverseMap();
        CreateMap<EditarUsuarioCommand, UsuarioModel>().ReverseMap();
        CreateMap<ObterUsuarioResultadoDTO, UsuarioModel>()
            .ForMember(dest => dest.NomeUsuario, opt => opt.MapFrom(src => src.NomeUsuario))
            .ForMember(dest => dest.EmailUsuario, opt => opt.MapFrom(src => src.EmailUsuario))
            .ForMember(dest => dest.Apelido, opt => opt.MapFrom(src => src.Apelido))
            .ForMember(dest => dest.BiografiaUsuario, opt => opt.MapFrom(src => src.BiografiaUsuario))
            .ForMember(dest => dest.ImagemUsuario, opt => opt.MapFrom(src => src.ImagemUsuario))
            .ForMember(dest => dest.Publico, opt => opt.MapFrom(src => src.Publico));
        CreateMap<ExcluirUsuarioCommand, UsuarioModel>().ReverseMap();
        CreateMap<ObterUsuarioCommand, ObterUsuarioParametrosDTO>().ReverseMap();
        CreateMap<ObterUsuarioByIdCommand, ObterUsuarioByIdParametrosDTO>().ReverseMap();
        CreateMap<ObterUsuarioResultadoDTO, ObterUsuarioRespostaDTO>().ReverseMap();
        CreateMap<PaginacaoResposta<ObterUsuarioResultadoDTO>, PaginacaoResposta<ObterUsuarioRespostaDTO>>();
    }
}