using ImpressioApi_.Domain.Queries;

namespace ImpressioApi_.Domain.DTO.Queries;

public class ObterUsuarioParametrosDTO : PaginacaoRequisicao
{
    public int? IdUsuario { get; set; }
    public string? EmailUsuario { get; set; }
    public DateTime? DataNascimento { get; set; }
    public string? Apelido { get; set; }
    public string? NomeUsuario { get; set; }
    public string? BiografiaUsuario { get; set; }
    public string? ImagemUsuario { get; set; }
    public bool? Publico { get; set; }
}