using ImpressioApi_.Application.Commands.Usuario.Validations;

namespace ImpressioApi_.Application.Commands.Usuario.Write;

public class CadastrarUsuarioCommand : Command<CommandResult>
{
    public required string EmailUsuario { get; set; }
    public required string Senha { get; set; }
    public required DateTime DataNascimento { get; set; }
    public required string Apelido { get; set; }
    public required string NomeUsuario { get; set; }
    
    public override async Task<bool> Valida()
    {
        var validation = new CadastrarUsuarioCommandValidation();
        _validationResult = await validation.ValidateAsync(this);

        return _validationResult.IsValid;
    }
}