namespace ImpressioApi_.Domain.DTO.Read;

public class ObterObraArteRespostaDTO
{
    public int? IdObraArte { get; set; }
    public required string ImagemObraArte { get; set; }
    public string? DescricaoObraArte { get; set; }
    public bool Publico { get; set; }
    public int? IdUsuario { get; set; }
    public string? NomeUsuario { get; set; }
    public string? Apelido { get; set; }
    public string? ImagemUsuario { get; set; }
}