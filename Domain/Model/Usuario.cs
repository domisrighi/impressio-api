using ImpressioApi_.Domain.Interfaces.Repositories;

namespace ImpressioApi_.Domain.Model;
public class UsuarioModel : IAggregateRoot
{
    public int IdUsuario { get; set; }
    public required string EmailUsuario { get; set; }
    public required string Senha { get; set; }
    public required DateTime DataNascimento { get; set; }
    public string? Apelido { get; set; }
    public string? NomeUsuario { get; set; }
    public string? BiografiaUsuario { get; set; }
    public string? ImagemUsuario { get; set; }
    public bool Publico { get; set; } = true;
    public ICollection<ObraArteModel> ObrasArte { get; set; } = new List<ObraArteModel>();
}