using ImpressioApi_.Domain.Interfaces.Repositories;

namespace ImpressioApi_.Domain.Model
{
    public class ObraArteModel : IAggregateRoot
    {
        public int IdObraArte { get; set; }
        public required string ImagemObraArte { get; set; }
        public string? DescricaoObraArte { get; set; }
        public bool Publico { get; set; } = true;
        public int? Upvote { get; set; } = 0;
        public int? Downvote { get; set; } = 0;
        public int IdUsuario { get; set; }
        public required UsuarioModel Usuario { get; set; }
        public virtual ICollection<ObraFavoritadaModel> UsuariosFavoritaram { get; set; } = new List<ObraFavoritadaModel>();
    }
}
