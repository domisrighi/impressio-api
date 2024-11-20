using ImpressioApi_.Domain.Queries;

namespace ImpressioApi_.Domain.DTO.Queries;

public class ObterObraArteFavoritaByIdParametrosDTO : PaginacaoRequisicao
{
    public int IdUsuario { get; set; }
    public int IdObraArte { get; set; }
}