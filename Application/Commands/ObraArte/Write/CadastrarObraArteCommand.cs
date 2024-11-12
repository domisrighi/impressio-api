using ImpressioApi_.Application.Commands.ObraArte.Validations;

namespace ImpressioApi_.Application.Commands.ObraArte.Write;

public class CadastrarObraArteCommand : Command<CommandResult>
{
    public int IdObraArte { get; set; }
    public int IdUsuario { get; set; }
    public required string ImagemObraArte { get; set; }
    public string? DescricaoObraArte { get; set; }
    public bool Publico { get; set; } = true;
    
    public override async Task<bool> Valida()
    {
        var validation = new CadastrarObraArteCommandValidation();
        _validationResult = await validation.ValidateAsync(this);

        return _validationResult.IsValid;
    }
}