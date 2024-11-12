using Dapper;
using ImpressioApi_.Domain.Interfaces.Repositories;
using ImpressioApi_.Domain.Model;
using ImpressioApi_.Infrastructure.Data.Contexts;
using Microsoft.EntityFrameworkCore;

namespace ImpressioApi_.Infrastructure.Data.Repositories
{
    public class ObraArteFavoritaRepository : Repository<ObraFavoritadaModel>, IObraArteFavoritaRepository
    {
        private readonly ImpressioDbContext _dbContext;

        public ObraArteFavoritaRepository(ImpressioDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
