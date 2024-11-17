using ImpressioApi_.Application.Commands.ObraArteFavorita.Validations;
using ImpressioApi_.Domain.DTO.Read;
using ImpressioApi_.Domain.Queries;

namespace ImpressioApi_.Application.Commands.ObraArteFavorita.Read;

public class ObterObraArteFavoritaCommand : PaginacaoCommand<CommandResult<PaginacaoResposta<ObterObraArteFavoritaRespostaDTO>>>
{
    public int IdUsuario { get; set; }
    public override async Task<bool> Valida()
    {
        var validation = new ObterObraArteFavoritaCommandValidation();
        _validationResult = await validation.ValidateAsync(this);

        return _validationResult.IsValid;
    }
}