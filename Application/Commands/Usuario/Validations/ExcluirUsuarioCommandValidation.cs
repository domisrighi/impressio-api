using FluentValidation;
using ImpressioApi_.Application.Commands.Usuario.Write;

namespace ImpressioApi_.Application.Commands.Usuario.Validations;

public class ExcluirUsuarioCommandValidation: AbstractValidator<ExcluirUsuarioCommand>
{
    public ExcluirUsuarioCommandValidation()
    {
        RuleFor(p => p.IdUsuario)
            .NotEmpty()
            .WithMessage("O Id do usuário é obrigatório.");
    }
}