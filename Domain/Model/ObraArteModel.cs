using ImpressioApi_.Domain.Interfaces.Repositories;

namespace ImpressioApi_.Domain.Model
{
    public class ObraArteModel : IAggregateRoot
    {
        public int IdObraArte { get; set; }
        public required string ImagemObraArte { get; set; }
        public string? DescricaoObraArte { get; set; }
        public bool Publico { get; set; } = true;
        public int IdUsuario { get; set; }
        public required UsuarioModel Usuario { get; set; }
        public virtual ICollection<ObraFavoritadaModel> UsuariosFavoritaram { get; set; } = new List<ObraFavoritadaModel>();
        public ICollection<ObraVotoModel> Votos { get; set; } = new List<ObraVotoModel>();
    }
}
