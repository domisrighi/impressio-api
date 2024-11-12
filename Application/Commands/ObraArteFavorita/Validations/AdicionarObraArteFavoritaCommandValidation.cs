using FluentValidation;

namespace ImpressioApi_.Application.Commands.ObraArteFavorita.Validations;

public class AdicionarObraArteFavoritaCommandValidation : AbstractValidator<AdicionarObraArteFavoritaCommand>
{
    public AdicionarObraArteFavoritaCommandValidation()
    {
        RuleFor(p => p.IdObraArte)
            .GreaterThan(0)
            .WithMessage("O ID da obra de arte é obrigatório.");
        RuleFor(p => p.IdUsuario)
            .GreaterThan(0)
            .WithMessage("O ID do usuário é obrigatório.");
    }
}
