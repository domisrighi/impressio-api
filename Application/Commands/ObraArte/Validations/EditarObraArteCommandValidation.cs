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
        RuleFor(p => p.DescricaoObraArte)
            .MaximumLength(170)
            .WithMessage("A descrição da obra de arte não pode exceder 170 caracteres.");
    }
}