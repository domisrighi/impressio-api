using System.Data;
using Dapper;
using ImpressioApi_.Domain.DTO.Queries;
using ImpressioApi_.Domain.Interfaces.Queries;
using ImpressioApi_.Domain.Queries;
using ImpressioApi_.Infrastructure.Data.Contexts;
using Microsoft.EntityFrameworkCore;

namespace ImpressioApi_.Infrastructure.Data.Queries;

public class ObterObraArteFavoritaQuery : IObterObraArteFavoritaQuery
{
  private readonly ILogger<ObterObraArteFavoritaQuery> _logger;
  private readonly IDbConnection _connection;

  public ObterObraArteFavoritaQuery(ILogger<ObterObraArteFavoritaQuery> logger, ImpressioDbContext impressioDbContext)
  {
    _logger = logger ?? throw new ArgumentNullException();
    _connection = impressioDbContext.Database.GetDbConnection();
  }

  public async Task<ObterObraArteFavoritaResultadoDTO?> ObterObraArteFavoritaByUsuarioEObraArte(int idObraArte, int idUsuario)
  {
    var sql = @$"{BuscarObraArteFavorita()}
                  WHERE id_obra_arte = @IdObraArte
                  AND (id_usuario = @IdUsuario)
                  ";

    var parametros = new { IdObraArte = idObraArte, IdUsuario = idUsuario };

    var favorito = await _connection.QueryFirstOrDefaultAsync<ObterObraArteFavoritaResultadoDTO>(sql, parametros);

    return favorito;
  }

  public async Task<PaginacaoResposta<ObterObraArteFavoritaResultadoDTO>> ObterObrasDeArteFavoritasByUsuario(ObterObraArteFavoritaParametrosDTO parametros)
  {
    var query = await _connection.QueryAsync<ObterObraArteFavoritaResultadoDTO>(
      BuscarObrasDeArtesFavoritas(), 
      new { IdUsuario = parametros.IdUsuario }
    );

    var paginacao = new PaginacaoResposta<ObterObraArteFavoritaResultadoDTO>(registros: query);

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

  public async Task<PaginacaoResposta<ObterObraArteFavoritaResultadoDTO>> ObterObrasDeArteFavoritasByObraFavoritada(ObterObraArteFavoritaByIdParametrosDTO parametros)
  {
    var query = await _connection.QueryAsync<ObterObraArteFavoritaResultadoDTO>(
      BuscarObrasDeArtesFavoritasById(), 
      new { IdObraFavoritada = parametros.IdObraFavoritada }
    );

    var paginacao = new PaginacaoResposta<ObterObraArteFavoritaResultadoDTO>(registros: query);

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

  private static string BuscarObrasDeArtesFavoritas() => @"SELECT 
                                                            obraFavoritada.id_obra_favoritada AS IdObraFavoritada,
                                                            obraArte.id_obra_arte AS IdObraArte,
                                                            obraArte.imagem_obra_arte AS ImagemObraArte,
                                                            obraArte.descricao_obra_arte AS DescricaoObraArte,
                                                            obraArte.id_usuario AS IdUsuario,
                                                            donoObra.nome_usuario AS NomeUsuario,
                                                            donoObra.apelido AS Apelido,
                                                            donoObra.imagem_usuario AS ImagemUsuario
                                                          FROM t_obra_favoritada obraFavoritada
                                                          INNER JOIN t_obra_arte obraArte ON obraFavoritada.id_obra_arte = obraArte.id_obra_arte
                                                          INNER JOIN t_usuario donoObra ON obraArte.id_usuario = donoObra.id_usuario
                                                          WHERE (@IdUsuario IS NULL OR obraFavoritada.id_usuario = @IdUsuario)
                                                          ORDER BY obraFavoritada.id_obra_favoritada
                                                          ";

  private static string BuscarObrasDeArtesFavoritasById() => @"SELECT 
                                                            obraFavoritada.id_obra_favoritada AS IdObraFavoritada,
                                                            obraArte.id_obra_arte AS IdObraArte,
                                                            obraArte.imagem_obra_arte AS ImagemObraArte,
                                                            obraArte.descricao_obra_arte AS DescricaoObraArte,
                                                            obraArte.id_usuario AS IdUsuario,
                                                            donoObra.nome_usuario AS NomeUsuario,
                                                            donoObra.apelido AS Apelido,
                                                            donoObra.imagem_usuario AS ImagemUsuario
                                                          FROM t_obra_favoritada obraFavoritada
                                                          INNER JOIN t_obra_arte obraArte ON obraFavoritada.id_obra_arte = obraArte.id_obra_arte
                                                          INNER JOIN t_usuario donoObra ON obraArte.id_usuario = donoObra.id_usuario
                                                          WHERE (@IdObraFavoritada IS NULL OR obraFavoritada.id_obra_favoritada = @IdObraFavoritada)
                                                          ORDER BY obraFavoritada.id_obra_favoritada
                                                          ";

  private static string BuscarObraArteFavorita() => @"SELECT
                                                      id_obra_favoritada AS IdObraFavoritada,
                                                      id_obra_arte AS IdObraArte,
                                                      id_usuario AS IdUsuario
                                                    FROM t_obra_favoritada
                                                    ";
}