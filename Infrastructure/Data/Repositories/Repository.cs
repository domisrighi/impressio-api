using ImpressioApi_.Domain.Interfaces.Repositories;
using ImpressioApi_.Infrastructure.Data.Contexts;
using Microsoft.EntityFrameworkCore;

namespace ImpressioApi_.Infrastructure.Data.Repositories;

public class Repository<T> : IRepository<T> where T : IAggregateRoot
{
    protected readonly ImpressioDbContext DbContext;
    protected readonly DbSet<T> DbSet;

    public IUnitOfWork UnitOfWork => DbContext;

    public Repository(ImpressioDbContext dbContext)
    {
        DbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        DbSet = DbContext.Set<T>();
    }

    public T Add(T entity)
    {
        return DbSet.Add(entity).Entity;
    }

    public void Update(T entity)
    {
        DbSet.Update(entity);
    }

    public async Task<T?> GetById(int id)
    {
        return await DbSet.FindAsync(id);
    }

    public async Task<List<T>> GetAll()
    {
        return await DbSet.ToListAsync();
    }

    public void Deletar(T entity)
    {
        DbSet.Remove(entity);
    }
    
    public void Dispose()
    {
        DbContext.DisposeAsync();
    }
}