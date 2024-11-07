using FluentValidation;
using ImpressioApi_.Application.Commands.Usuario.Write;

namespace ImpressioApi_.Application.Commands.Login.Validations;

public class LoginUsuarioCommandValidation : AbstractValidator<LoginUsuarioCommand>
{
    public LoginUsuarioCommandValidation()
    {
        RuleFor(p => p.EmailUsuario)
            .NotEmpty()
            .WithMessage("O E-mail é obrigatório.");
        RuleFor(p => p.Senha)
            .NotEmpty()
            .WithMessage("A senha é obrigatória.");
    }
}