using ImpressioApi_.Domain.DTO.Read;
using MediatR;

namespace ImpressioApi_.Application.Commands.Usuario.Read
{
    public class ObterUsuarioByIdCommand : IRequest<CommandResult<ObterUsuarioRespostaDTO>>
    {
        public int IdUsuario { get; set; }
    }
}
