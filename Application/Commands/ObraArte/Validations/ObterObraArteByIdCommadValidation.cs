using FluentValidation;
using ImpressioApi_.Application.Commands.ObraArte.Read;

namespace ImpressioApi_.Application.Commands.ObraArte.Validations;

public class ObterObraArteByIdCommandValidation : AbstractValidator<ObterObraArteByIdCommand>
{
    public ObterObraArteByIdCommandValidation()
    {

    }
}