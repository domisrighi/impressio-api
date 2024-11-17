using ImpressioApi_.Domain.Queries;

namespace ImpressioApi_.Domain.DTO.Queries;

public class ObterObraArteFavoritaParametrosDTO : PaginacaoRequisicao
{
    public int IdUsuario { get; set; }
}