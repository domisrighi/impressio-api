using ImpressioApi_.Application.Commands;
using ImpressioApi_.Application.Commands.RegistrarVoto.Validations;

public class RegistrarVotoCommand : Command<CommandResult>
{
    public int IdUsuario { get; set; }
    public int IdObraArte { get; set; }
    public ReacaoStatus Voto { get; set; }
    
    public override async Task<bool> Valida()
    {
        var validation = new RegistrarVotoCommandValidation();
        _validationResult = await validation.ValidateAsync(this);

        return _validationResult.IsValid;
    }
}