using Dapper;
using ImpressioApi_.Domain.Interfaces.Repositories;
using ImpressioApi_.Domain.Model;
using ImpressioApi_.Infrastructure.Data.Contexts;
using Microsoft.EntityFrameworkCore;

namespace ImpressioApi_.Infrastructure.Data.Repositories
{
    public class UsuarioRepository : Repository<UsuarioModel>, IUsuarioRepository
    {
        private readonly ImpressioDbContext _dbContext;

        public UsuarioRepository(ImpressioDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<UsuarioModel?> ObterPorEmail(string email)
        {
            var query = @"SELECT email_usuario
                            FROM t_usuario
                            WHERE email_usuario = @EmailUsuario";

            using var connection = _dbContext.Database.GetDbConnection();

            return await connection.QueryFirstOrDefaultAsync<UsuarioModel>(query, new { EmailUsuario = email });
        }
    }
}
