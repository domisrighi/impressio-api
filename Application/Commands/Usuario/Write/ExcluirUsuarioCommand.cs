using ImpressioApi_.Application.Commands.Usuario.Validations;

namespace ImpressioApi_.Application.Commands.Usuario.Write;

public class ExcluirUsuarioCommand : Command<CommandResult>
{
    public int IdUsuario { get; set; }

    public override async Task<bool> Valida()
    {
        var validation = new ExcluirUsuarioCommandValidation();
        _validationResult = await validation.ValidateAsync(this);
        
        return _validationResult.IsValid;
    }
}