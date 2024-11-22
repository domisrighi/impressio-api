using Dapper;
using ImpressioApi_.Domain.DTO.Read;
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
    }
}
