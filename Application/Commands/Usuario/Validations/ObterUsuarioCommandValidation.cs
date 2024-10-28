using FluentValidation;
using ImpressioApi_.Application.Commands.Usuario.Read;

namespace ImpressioApi_.Application.Commands.Usuario.Validations;

public class ObterUsuarioCommandValidation : AbstractValidator<ObterUsuarioCommand>
{
    public ObterUsuarioCommandValidation()
    {

    }
}