using ImpressioApi_.Application.Commands.ObraArte.Validations;
using ImpressioApi_.Application.Commands.ObraArteFavorita.Read;
using ImpressioApi_.Domain.DTO.Read;
using ImpressioApi_.Domain.Queries;

namespace ImpressioApi_.Application.Commands.Usuario.Read;

public class ObterObraArteCommand : PaginacaoCommand<CommandResult<PaginacaoResposta<ObterObraArteRespostaDTO>>>
{
    public string? DescricaoObraArte { get; set; }
    public bool? Publico { get; set; }
    public int? IdUsuario { get; set; }

    public override async Task<bool> Valida()
    {
        var validation = new ObterObraArteCommandValidation();
        _validationResult = await validation.ValidateAsync(this);

        return _validationResult.IsValid;
    }
}