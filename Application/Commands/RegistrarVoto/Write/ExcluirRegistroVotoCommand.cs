using ImpressioApi_.Application.Commands.RegistrarVoto.Validations;

namespace ImpressioApi_.Application.Commands.RegistrarVoto.Write;

public class ExcluirRegistroVotoCommand : Command<CommandResult>
{
    public int IdObraVoto { get; set; }

    public override async Task<bool> Valida()
    {
        var validation = new ExcluirRegistroVotoCommandValidation();
        _validationResult = await validation.ValidateAsync(this);
        
        return _validationResult.IsValid;
    }
}