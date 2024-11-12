using ImpressioApi_.Domain.Interfaces.Repositories;

namespace ImpressioApi_.Domain.Model;
public class ObraVotoModel : IAggregateRoot
{
    public int IdObraVoto { get; set; }
    public int IdObraArte { get; set; }
    public int IdUsuario { get; set; }
    public int Voto { get; set; }
    public virtual ObraArteModel? ObraArte { get; set; }
    public virtual UsuarioModel? Usuario { get; set; }
}