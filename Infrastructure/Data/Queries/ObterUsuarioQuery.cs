using System.Data;
using Dapper;
using ImpressioApi_.Domain.DTO.Queries;
using ImpressioApi_.Domain.Interfaces.Queries;
using ImpressioApi_.Domain.Queries;
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
      var query = await BuscarUsuarios(parametros);
        var paginacao = new PaginacaoResposta<ObterUsuarioResultadoDTO>(registros: query);

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
  private async Task<IEnumerable<ObterUsuarioResultadoDTO>> BuscarUsuarios(ObterUsuarioParametrosDTO parametros)
  {
    var sql = @"SELECT 
                DISTINCT
                  u.id_usuario AS IdUsuario,
                  u.nome_usuario AS NomeUsuario,
                  u.email_usuario AS EmailUsuario,
                  u.data_nascimento AS DataNascimento,
                  u.apelido AS Apelido,
                  u.biografia_usuario AS BiografiaUsuario,
                  u.imagem_usuario AS ImagemUsuario,
                  u.publico AS Publico
                FROM t_usuario AS u
                WHERE
                  (@NomeUsuario IS NULL OR u.nome_usuario LIKE CONCAT('%', @NomeUsuario, '%'))
                  AND (@EmailUsuario IS NULL OR u.email_usuario LIKE CONCAT('%', @EmailUsuario, '%'))
                  AND (@Apelido IS NULL OR u.apelido LIKE CONCAT('%', @Apelido, '%'))
                  AND (@Publico IS NULL OR u.publico = @Publico)
              ";

    var filtros = new
    {
      EmailUsuario = parametros.EmailUsuario,
      NomeUsuario = parametros.NomeUsuario,
      Apelido = parametros.Apelido,
      Publico = parametros.Publico
    };

    return await _connection.QueryAsync<ObterUsuarioResultadoDTO>(sql, filtros);
  }
}