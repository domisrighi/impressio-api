using FluentValidation;
using ImpressioApi_.Application.Commands.ObraArte.Write;

namespace ImpressioApi_.Application.Commands.ObraArte.Validations;

public class ExcluirObraArteCommandValidation: AbstractValidator<ExcluirObraArteCommand>
{
    public ExcluirObraArteCommandValidation()
    {
        RuleFor(p => p.IdObraArte)
            .NotEmpty()
            .WithMessage("O Id da obra de arte é obrigatório.");
    }
}