using ImpressioApi_.Application.Commands.ObraArte.Validations;
using ImpressioApi_.Domain.DTO.Write;

namespace ImpressioApi_.Application.Commands.ObraArte.Write;

public class EditarObraArteCommand : Command<CommandResult<EditarObraArteRespostaDTO>>
{
    public required int IdObraArte { get; set; }
    public string? DescricaoObraArte { get; set; }
    public bool Publico { get; set; }

    public override async Task<bool> Valida()
    {
        var validation = new EditarObraArteCommandValidation();
        _validationResult = await validation.ValidateAsync(this);

        return _validationResult.IsValid;
    }
}