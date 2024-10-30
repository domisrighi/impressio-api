using AutoMapper;
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
        CreateMap<ExcluirUsuarioCommand, UsuarioModel>().ReverseMap();
        CreateMap<ObterUsuarioCommand, ObterUsuarioParametrosDTO>().ReverseMap();
        CreateMap<ObterUsuarioResultadoDTO, ObterUsuarioRespostaDTO>().ReverseMap();
        CreateMap<PaginacaoResposta<ObterUsuarioResultadoDTO>, PaginacaoResposta<ObterUsuarioRespostaDTO>>();
    }
}