using FluentValidation;
using ImpressioApi_.Application.Commands.ObraArte.Write;

namespace ImpressioApi_.Application.Commands.ObraArte.Validations;

public class CadastrarObraArteCommandValidation : AbstractValidator<CadastrarObraArteCommand>
{
    public CadastrarObraArteCommandValidation()
    {
        RuleFor(p => p.ImagemObraArte)
            .NotEmpty()
            .WithMessage("A imagem da obra de arte é obrigatória.");

        RuleFor(p => p.DescricaoObraArte)
            .MaximumLength(170)
            .WithMessage("A descrição da obra de arte não pode exceder 170 caracteres.");

        RuleFor(p => p.IdUsuario)
            .GreaterThan(0)
            .WithMessage("O ID do usuário associado à obra de arte é obrigatório.");
    }
}
