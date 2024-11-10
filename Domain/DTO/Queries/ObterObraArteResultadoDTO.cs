namespace ImpressioApi_.Domain.DTO.Queries;

public class ObterObraArteResultadoDTO
{
        public int IdObraArte { get; set; }
        public required string ImagemObraArte { get; set; }
        public string? DescricaoObraArte { get; set; }
        public bool Publico { get; set; }
        public int IdUsuario { get; set; }
}