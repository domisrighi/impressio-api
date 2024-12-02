using ImpressioApi_.Domain.Interfaces.Repositories;
using ImpressioApi_.Domain.Model;
using ImpressioApi_.Infrastructure.Data.Contexts;

namespace ImpressioApi_.Infrastructure.Data.Repositories
{
    public class ObraArteRepository : Repository<ObraArteModel>, IObraArteRepository
    {
        private readonly ImpressioDbContext _dbContext;

        public ObraArteRepository(ImpressioDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
