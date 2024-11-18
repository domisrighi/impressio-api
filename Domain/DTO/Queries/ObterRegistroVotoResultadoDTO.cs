namespace ImpressioApi_.Domain.DTO.Queries
{
    public class ObterRegistroVotoResultadoDTO
    {
        public int IdObraVoto { get; set; }
        public int IdUsuario { get; set; }
        public int IdObraArte { get; set; }
        public int Voto { get; set; }
    }
}