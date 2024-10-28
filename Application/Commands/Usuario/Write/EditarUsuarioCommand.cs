using ImpressioApi_.Application.Commands.Usuario.Validations;
using ImpressioApi_.Domain.DTO.Write;

namespace ImpressioApi_.Application.Commands.Usuario.Write;

public class EditarUsuarioCommand : Command<CommandResult<EditarUsuarioRespostaDTO>>
{
    public required int IdUsuario { get; set; }
    public string? EmailUsuario { get; set; }
    public DateTime? DataNascimento { get; set; }
    public string? Senha { get; set; }
    public string? Apelido { get; set; }
    public string? NomeUsuario { get; set; }
    public string? BiografiaUsuario { get; set; }
    public string? ImagemUsuario { get; set; }
    public bool? Publico { get; set; }

    public override async Task<bool> Valida()
    {
        var validation = new EditarUsuarioCommandValidation();
        _validationResult = await validation.ValidateAsync(this);

        return _validationResult.IsValid;
    }
}