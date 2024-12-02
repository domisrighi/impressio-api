using ImpressioApi_.Application.Commands.ObraArte.Validations;

namespace ImpressioApi_.Application.Commands.ObraArte.Write;

public class ExcluirObraArteCommand : Command<CommandResult>
{
    public int IdObraArte { get; set; }

    public override async Task<bool> Valida()
    {
        var validation = new ExcluirObraArteCommandValidation();
        _validationResult = await validation.ValidateAsync(this);
        
        return _validationResult.IsValid;
    }
}