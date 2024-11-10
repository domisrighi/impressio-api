using System.Data;
using Dapper;
using ImpressioApi_.Domain.DTO.Queries;
using ImpressioApi_.Domain.Interfaces.Queries;
using ImpressioApi_.Domain.Queries;
using ImpressioApi_.Infrastructure.Data.Contexts;
using Microsoft.EntityFrameworkCore;

namespace ImpressioApi_.Infrastructure.Data.Queries;

public class ObterObraArteQuery : IObterObraArteQuery
{
  private readonly ILogger<ObterObraArteQuery> _logger;
  private readonly IDbConnection _connection;

  public ObterObraArteQuery(ILogger<ObterObraArteQuery> logger, ImpressioDbContext impressioDbContext)
  {
    _logger = logger ?? throw new ArgumentNullException();
    _connection = impressioDbContext.Database.GetDbConnection();
  }

  public async Task<PaginacaoResposta<ObterObraArteResultadoDTO>> ObterObraDeArte(ObterObraArteParametrosDTO parametros)
  {
    var query = await BuscarObrasDeArtes(parametros);
    var paginacao = new PaginacaoResposta<ObterObraArteResultadoDTO>(registros: query);

    if (!parametros.Paginar)
    {
        return paginacao;
    }

    var totalDeItens = query.Count();

    paginacao.PreencherPropriedades(
      totalDeItens: totalDeItens,
      paginaAtual: parametros.PaginaAtual,
      itensPorPagina: parametros.ItensPorPagina
    );

    return paginacao;
  }

  public async Task<ObterObraArteResultadoDTO?> ObterObraDeArteById(int idObraArte)
  {
    var sql = @$"{BuscarObras()}
                  WHERE id_obra_arte = @IdObraArte
                  ";

    var parametros = new { IdObraArte = idObraArte };

    var obraDeArte = await _connection.QueryFirstOrDefaultAsync<ObterObraArteResultadoDTO>(sql, parametros);

    return obraDeArte;
  }


  private async Task<IEnumerable<ObterObraArteResultadoDTO>> BuscarObrasDeArtes(ObterObraArteParametrosDTO parametros)
  {
    int itensIgnorados = (parametros.PaginaAtual - 1) * parametros.ItensPorPagina;

    var sql = $@"SELECT
                    oa.id_obra_arte AS IdObraArte,
                    oa.imagem_obra_arte AS ImagemObraArte,
                    oa.descricao_obra_arte AS DescricaoObraArte,
                    oa.publico AS Publico,
                    oa.id_usuario AS IdUsuario                    
                FROM t_obra_arte AS oa
                WHERE
                    (@DescricaoObraArte IS NULL OR oa.descricao_obra_arte LIKE CONCAT('%', @DescricaoObraArte, '%'))
                    AND (@Publico IS NULL OR oa.publico = @Publico)
                    AND (@IdUsuario IS NULL OR oa.id_usuario = @IdUsuario)
                    ORDER BY oa.id_obra_arte
                    OFFSET @ItensIgnorados ROWS
                    FETCH NEXT @ItensPorPagina ROWS ONLY
              ";

    var filtros = new
    {
      DescricaoObraArte = parametros.DescricaoObraArte,
      Publico = parametros.Publico,
      IdUsuario = parametros.IdUsuario,
      ItensIgnorados = itensIgnorados,
      ItensPorPagina = parametros.ItensPorPagina
    };

    return await _connection.QueryAsync<ObterObraArteResultadoDTO>(sql, filtros);
  }

  private static string BuscarObras() => @"SELECT
                                            id_obra_arte AS IdObraArte,
                                            imagem_obra_arte AS ImagemObraArte,
                                            descricao_obra_arte AS DescricaoObraArte,
                                            publico AS Publico,
                                            id_usuario AS IdUsuario
                                          FROM t_obra_arte
                                          ";
}