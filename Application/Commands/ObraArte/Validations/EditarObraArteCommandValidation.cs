using FluentValidation;
using ImpressioApi_.Application.Commands.ObraArte.Write;

namespace ImpressioApi_.Application.Commands.ObraArte.Validations;

public class EditarObraArteCommandValidation : AbstractValidator<EditarObraArteCommand>
{
    public EditarObraArteCommandValidation()
    {
        RuleFor(p => p.IdObraArte)
            .NotEmpty()
            .WithMessage("O Id da obra de arte é obrigatório.");
    }
    //TODO - Adicionar validações conforme campos editáveis.
}