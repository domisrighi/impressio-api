using FluentValidation;
using ImpressioApi_.Application.Commands.ObraArteFavorita.Write;

namespace ImpressioApi_.Application.Commands.ObraArteFavorita.Validations;

public class ExcluirObraArteFavoritaCommandValidation: AbstractValidator<ExcluirObraArteFavoritaCommand>
{
    public ExcluirObraArteFavoritaCommandValidation()
    {
        RuleFor(p => p.IdObraFavorita)
            .NotEmpty()
            .WithMessage("O Id da obra de arte favorita é obrigatório.");
    }
}