using FluentValidation;
using ImpressioApi_.Application.Commands.RegistrarVoto.Write;

namespace ImpressioApi_.Application.Commands.RegistrarVoto.Validations;

public class ExcluirRegistroVotoCommandValidation: AbstractValidator<ExcluirRegistroVotoCommand>
{
    public ExcluirRegistroVotoCommandValidation()
    {
        RuleFor(p => p.IdObraVoto)
            .NotEmpty()
            .WithMessage("O ID do registro de voto da obra de arte é obrigatório.");
    }
}