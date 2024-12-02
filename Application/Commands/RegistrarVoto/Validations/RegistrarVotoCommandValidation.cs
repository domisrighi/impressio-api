using FluentValidation;

namespace ImpressioApi_.Application.Commands.RegistrarVoto.Validations;

public class RegistrarVotoCommandValidation : AbstractValidator<RegistrarVotoCommand>
{
    public RegistrarVotoCommandValidation()
    {
        RuleFor(p => p.IdUsuario)
            .GreaterThan(0)
            .WithMessage("O ID do usuário é obrigatório.");
        RuleFor(p => p.IdObraArte)
            .GreaterThan(0)
            .WithMessage("O ID da obra de arte é obrigatório.");
        RuleFor(p => p.Voto)
            .IsInEnum()
            .WithMessage("O status do voto deve ser 'Gostei', 'Amei' ou 'Brilhante'.");    
    }
}
