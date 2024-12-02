using FluentValidation;
using ImpressioApi_.Application.Commands.Usuario.Write;

namespace ImpressioApi_.Application.Commands.Usuario.Validations;

public class CadastrarUsuarioCommandValidation : AbstractValidator<CadastrarUsuarioCommand>
{
    public CadastrarUsuarioCommandValidation()
    {
        RuleFor(p => p.EmailUsuario)
            .NotEmpty()
            .WithMessage("O E-mail é obrigatório.");
        RuleFor(p => p.Senha)
            .NotEmpty()
            .WithMessage("A senha é obrigatória.");
        RuleFor(p => p.DataNascimento)
            .NotEmpty()
            .WithMessage("A data de nascimento é obrigatória.");
        RuleFor(p => p.Apelido)
            .NotEmpty()
            .WithMessage("O apelido é obrigatório.");
        RuleFor(p => p.NomeUsuario)
            .NotEmpty()
            .WithMessage("O nome do usuário é obrigatório.");
    }
}