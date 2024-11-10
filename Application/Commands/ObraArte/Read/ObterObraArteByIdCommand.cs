using ImpressioApi_.Domain.DTO.Read;
using MediatR;

namespace ImpressioApi_.Application.Commands.ObraArte.Read
{
    public class ObterObraArteByIdCommand : IRequest<CommandResult<ObterObraArteRespostaDTO>>
    {
        public int IdObraArte { get; set; }
    }
}
