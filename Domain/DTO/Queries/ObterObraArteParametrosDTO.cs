using ImpressioApi_.Domain.Queries;

namespace ImpressioApi_.Domain.DTO.Queries;

public class ObterObraArteParametrosDTO : PaginacaoRequisicao
{
        public string? DescricaoObraArte { get; set; }
        public bool? Publico { get; set; }
        public int? IdUsuario { get; set; }
}