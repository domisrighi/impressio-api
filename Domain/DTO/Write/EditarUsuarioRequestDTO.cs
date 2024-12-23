namespace ImpressioApi_.Domain.DTO.Write;

public class EditarUsuarioRequestDTO
{
    public int IdUsuario { get; set; }
    public string? EmailUsuario { get; set; }
    public DateTime? DataNascimento { get; set; }
    public string? Apelido { get; set; }
    public string? NomeUsuario { get; set; }
    public string? BiografiaUsuario { get; set; }
    public string? ImagemUsuario { get; set; }
    public bool Publico { get; set; }
}