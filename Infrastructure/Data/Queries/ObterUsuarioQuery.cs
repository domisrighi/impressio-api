using System.Data;
using Dapper;
using ImpressioApi_.Domain.DTO.Queries;
using ImpressioApi_.Domain.Interfaces.Queries;
using ImpressioApi_.Domain.Queries;
using ImpressioApi_.Infrastructure.Data.Configurations;
using ImpressioApi_.Infrastructure.Data.Contexts;
using Microsoft.EntityFrameworkCore;

namespace ImpressioApi_.Infrastructure.Data.Queries;

public class ObterUsuarioQuery : IObterUsuarioQuery
{
  private readonly ILogger<ObterUsuarioQuery> _logger;
  private readonly IDbConnection _connection;

  public ObterUsuarioQuery(ILogger<ObterUsuarioQuery> logger, ImpressioDbContext impressioDbContext)
  {
    _logger = logger ?? throw new ArgumentNullException();
    _connection = impressioDbContext.Database.GetDbConnection();
  }

  public async Task<PaginacaoResposta<ObterUsuarioResultadoDTO>> ObterUsuario(ObterUsuarioParametrosDTO parametros)
  {
    var select = @"SELECT
                    DISTINCT
                    t_usuario.id_usuario IdUsuario,
                    t_usuario.nome_usuario Nome,
                    t_usuario.email_usuario Email,
                    t_usuario.data_nascimento DataNascimento,
                    t_usuario.apelido Apelido,
                    t_usuario.biografia_usuario BiografiaUsuario,
                    t_usuario.imagem_usuario ImagemUsuario,
                    t_usuario.publico Publico
                ";
    
    var from = @"FROM
                  t_usuario t_usuario
                WHERE
                  (t_usuario.id_usuario = @IdUsuario OR @IdUsuario IS NULL)
                  AND (t_usuario.nome_usuario = @NomeUsuario OR NULLIF(@NomeUsuario, '') IS NULL)
                  AND (t_usuario.email_usuario = @EmailUsuario OR NULLIF(@EmailUsuario, '') IS NULL)
                  AND (t_usuario.data_nascimento = @DataNascimento OR @DataNascimento IS NULL)
                  AND (t_usuario.apelido = @Apelido OR NULLIF(@Apelido, '') IS NULL)
                  AND (t_usuario.biografia_usuario = @BiografiaUsuario OR NULLIF(@BiografiaUsuario, '') IS NULL)
                  AND (t_usuario.imagem_usuario = @ImagemUsuario OR NULLIF(@ImagemUsuario, '') IS NULL)
                  AND (t_usuario.publico = @Publico OR @Publico IS NULL)
                ";
    
    var sql = @$"{select}
                      {from}";

    if (parametros.Paginar)
    {
      sql += @" LIMIT @ItensPorPagina
                OFFSET @ItensIgnorados
                ";
    }

    var filtros = new
    {
      parametros.IdUsuario,
      parametros.EmailUsuario,
      parametros.DataNascimento,
      parametros.NomeUsuario,
      parametros.Apelido,
      parametros.BiografiaUsuario,
      parametros.ImagemUsuario,
      parametros.Publico,
      parametros.ItensPorPagina,
      ItensIgnorados = parametros.ItensIgnorados()
    };

    var result = await _connection.QueryAsync<ObterUsuarioResultadoDTO, ObterUsuarioQuery>(_logger, sql, filtros);
    var paginacao = new PaginacaoResposta<ObterUsuarioResultadoDTO>(registros: result);

    if (!parametros.Paginar)
    {
      return paginacao;
    }

    var sqlTotal = @$"SELECT COUNT(*)
                        FROM (
                        {select}
                        {from}
                        ) query
                    ";

    var totalDeItens = await _connection.QuerySingleAsync<int, ObterUsuarioQuery>(_logger, sqlTotal, filtros);

    paginacao.PreencherPropriedades(
        totalDeItens: totalDeItens,
        paginaAtual: parametros.PaginaAtual,
        itensPorPagina: parametros.ItensPorPagina
    );
    
    return paginacao;
  }
}