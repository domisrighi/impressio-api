namespace ImpressioApi_.Domain.Queries;

public class PaginacaoResposta<T>
{
    public int TotalDeItens { get; set; }
    public int ItensPorPagina { get; set; }
    public int PaginaAtual { get; set; }
    public int TotalDePaginas
    {
        get
        {
            if (ItensPorPagina == 0)
            {
                return 0;
            }

            return (int)Math.Ceiling((double)TotalDeItens / (double)ItensPorPagina);
        }
    }

    public bool TemPaginaAnterior
    {
        get
        {
            return PaginaAtual > 1;
        }
    }

    public bool TemPaginaSeguinte
    {
        get
        {
            return PaginaAtual < TotalDePaginas;
        }
    }

    public IEnumerable<T> Registros { get; set; } = Enumerable.Empty<T>();

    public PaginacaoResposta()
    {

    }

    public PaginacaoResposta(int totalDeItens, int paginaAtual, int itensPorPagina, List<T> registros)
    {
        PreencherPropriedades(
            totalDeItens: totalDeItens,
            paginaAtual: paginaAtual,
            itensPorPagina: itensPorPagina
        );
        Registros = registros;
    }

    public PaginacaoResposta(IEnumerable<T> registros)
    {
        Registros = registros;
    }

    public PaginacaoResposta<T> PreencherPropriedades(int totalDeItens, int paginaAtual, int itensPorPagina)
    {
        TotalDeItens = totalDeItens;
        PaginaAtual = paginaAtual;
        ItensPorPagina = itensPorPagina;
        
        return this;
    }
}