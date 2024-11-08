namespace ImpressioApi_.Domain.DTO.Read;

public class ObterObraArteRespostaDTO
{
    public int? IdObraArte { get; set; }
    public required string ImagemObraArte { get; set; }
    public string? DescricaoObraArte { get; set; }
    public bool Publico { get; set; }
    public int? Upvote { get; set; }
    public int? Downvote { get; set; }
    public int? IdUsuario { get; set; }
}