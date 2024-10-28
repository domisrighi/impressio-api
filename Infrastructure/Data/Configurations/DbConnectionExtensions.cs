using System.Data;
using Dapper;

namespace ImpressioApi_.Infrastructure.Data.Configurations;

public static class DbConnectionExtensions
{
    public static Task<IEnumerable<TResult>> QueryAsync<TResult, TLog>(this IDbConnection cnn, ILogger<TLog> logger, string sql, object? param = null, IDbTransaction? transaction = null, int? commandTimeout = null, CommandType? commandType = null)
    {
        
        return cnn.QueryAsync<TResult>(sql, param, transaction, commandTimeout, commandType);
    }

    public static Task<TResult> QuerySingleAsync<TResult, TLog>(this IDbConnection cnn, ILogger<TLog> logger, string sql, object? param = null, IDbTransaction? transaction = null, int? commandTimeout = null, CommandType? commandType = null)
    {
       
        return cnn.QuerySingleAsync<TResult>(sql, param, transaction, commandTimeout, commandType);
    }
}