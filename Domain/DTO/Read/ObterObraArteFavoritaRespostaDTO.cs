namespace ImpressioApi_.Domain.DTO.Read;

public class ObterObraArteFavoritaRespostaDTO
{
    public int IdObraFavoritada { get; set; }
    public int IdObraArte { get; set; }
    public string? ImagemObraArte { get; set; }
    public string? DescricaoObraArte { get; set; }
    public int IdUsuario { get; set; }
    public string? NomeUsuario { get; set; }
    public string? Apelido { get; set; }
    public string? ImagemUsuario { get; set; }
}