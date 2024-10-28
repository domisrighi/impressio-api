using System.ComponentModel;

namespace ImpressioApi_.Domain.Queries;

public class PaginacaoRequisicao
{
    [DefaultValue(1)]
    public int PaginaAtual { get; set; } = 1;

    [DefaultValue(100)]
    public int ItensPorPagina { get; set; } = 100;

    [DefaultValue(true)]
    public bool Paginar { get; set; } = true;
    
    public int ItensIgnorados()
    { 
        return (PaginaAtual - 1) * ItensPorPagina; 
    }
}