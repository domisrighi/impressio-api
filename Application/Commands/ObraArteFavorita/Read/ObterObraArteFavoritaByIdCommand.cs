using ImpressioApi_.Application.Commands.ObraArteFavorita.Validations;
using ImpressioApi_.Domain.DTO.Read;
using ImpressioApi_.Domain.Queries;

namespace ImpressioApi_.Application.Commands.ObraArteFavorita.Read;

public class ObterObraArteFavoritaByIdCommand : PaginacaoCommand<CommandResult<PaginacaoResposta<ObterObraArteFavoritaRespostaDTO>>>
{
    public int IdUsuario { get; set; }
    public int IdObraArte { get; set; }
    public override async Task<bool> Valida()
    {
        var validation = new ObterObraArteFavoritaByIdCommandValidation();
        _validationResult = await validation.ValidateAsync(this);

        return _validationResult.IsValid;
    }
}