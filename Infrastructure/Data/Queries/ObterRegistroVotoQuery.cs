using System.Data;
using Dapper;
using ImpressioApi_.Domain.DTO.Queries;
using ImpressioApi_.Domain.Interfaces.Queries;
using ImpressioApi_.Infrastructure.Data.Contexts;
using Microsoft.EntityFrameworkCore;

namespace ImpressioApi_.Infrastructure.Data.Queries;

public class ObterRegistroVotoQuery : IObterRegistroVotoQuery
{
    private readonly ILogger<ObterRegistroVotoQuery> _logger;
    private readonly IDbConnection _connection;

    public ObterRegistroVotoQuery(ILogger<ObterRegistroVotoQuery> logger, ImpressioDbContext impressioDbContext)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _connection = impressioDbContext.Database.GetDbConnection();
    }

    public async Task<ObterRegistroVotoResultadoDTO?> ObterRegistroVotoByObraArteEUsuario(int idObraArte, int idUsuario)
    {
        var sql = @$"{BuscarRegistroVoto()}
                        WHERE id_obra_arte = @IdObraArte
                        AND id_usuario = @IdUsuario
                    ";

        var parametros = new { IdObraArte = idObraArte, IdUsuario = idUsuario };

        try
        {
            var registroVoto = await _connection.QueryFirstOrDefaultAsync<ObterRegistroVotoResultadoDTO>(sql, parametros);
            return registroVoto;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao buscar registro de voto para ObraArteId: {IdObraArte}, UsuarioId: {IdUsuario}", idObraArte, idUsuario);
            throw;
        }
    }

    private static string BuscarRegistroVoto() => @"SELECT 
                                                        id_obra_voto AS IdObraVoto,
                                                        id_obra_arte AS IdObraArte,
                                                        id_usuario AS IdUsuario,
                                                        voto AS VotoStatus
                                                    FROM t_obra_voto
                                                    ";
}
