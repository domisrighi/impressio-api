using FluentValidation;
using ImpressioApi_.Application.Commands.Usuario.Write;

namespace ImpressioApi_.Application.Commands.Usuario.Validations;

public class EditarUsuarioCommandValidation : AbstractValidator<EditarUsuarioCommand>
{
    public EditarUsuarioCommandValidation()
    {
        RuleFor(p => p.IdUsuario)
            .NotEmpty()
            .WithMessage("O Id do usuário é obrigatório.");
    }
    //TODO - Adicionar validações conforme campos editáveis.
}