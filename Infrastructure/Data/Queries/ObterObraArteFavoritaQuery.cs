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

  private static string BuscarObraArteFavorita() => @"SELECT
                                                      id_obra_favoritada AS IdObraFavoritada,
                                                      id_obra_arte AS IdObraArte,
                                                      id_usuario AS IdUsuario
                                                    FROM t_obra_favoritada
                                                    ";
}