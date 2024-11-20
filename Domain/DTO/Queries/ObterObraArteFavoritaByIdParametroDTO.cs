using ImpressioApi_.Domain.Queries;

namespace ImpressioApi_.Domain.DTO.Queries;

public class ObterObraArteFavoritaByIdParametrosDTO : PaginacaoRequisicao
{
    public int IdObraFavoritada { get; set; }
}