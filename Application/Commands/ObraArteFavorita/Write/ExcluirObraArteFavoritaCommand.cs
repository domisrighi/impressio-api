using ImpressioApi_.Application.Commands.ObraArteFavorita.Validations;

namespace ImpressioApi_.Application.Commands.ObraArteFavorita.Write;

public class ExcluirObraArteFavoritaCommand : Command<CommandResult>
{
    public int IdObraFavorita { get; set; }

    public override async Task<bool> Valida()
    {
        var validation = new ExcluirObraArteFavoritaCommandValidation();
        _validationResult = await validation.ValidateAsync(this);
        
        return _validationResult.IsValid;
    }
}