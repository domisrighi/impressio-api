using ImpressioApi_.Application.Commands.Usuario.Validations;
using ImpressioApi_.Domain.DTO.Read;
using ImpressioApi_.Domain.Queries;

namespace ImpressioApi_.Application.Commands.Usuario.Read;

public class ObterUsuarioCommand : PaginacaoCommand<CommandResult<PaginacaoResposta<ObterUsuarioRespostaDTO>>>
{
    public string? EmailUsuario { get; set; }
    public string? Apelido { get; set; }
    public string? NomeUsuario { get; set; }
    public bool? Publico { get; set; }

    public override async Task<bool> Valida()
    {
        var validation = new ObterUsuarioCommandValidation();
        _validationResult = await validation.ValidateAsync(this);

        return _validationResult.IsValid;
    }
}