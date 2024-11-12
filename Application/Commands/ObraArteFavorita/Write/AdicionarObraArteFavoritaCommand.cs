using ImpressioApi_.Application.Commands;
using ImpressioApi_.Application.Commands.ObraArteFavorita.Validations;

public class AdicionarObraArteFavoritaCommand : Command<CommandResult>
{
    public int IdObraArteFavoritada { get; set; }
    public int IdObraArte { get; set; }
    public int IdUsuario { get; set; }
    
    public override async Task<bool> Valida()
    {
        var validation = new AdicionarObraArteFavoritaCommandValidation();
        _validationResult = await validation.ValidateAsync(this);

        return _validationResult.IsValid;
    }
}