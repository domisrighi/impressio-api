namespace ImpressioApi_.Domain.Interfaces.Repositories;

public interface IRepository<T> : IDisposable where T : IAggregateRoot
{
    IUnitOfWork UnitOfWork { get; }
    Task<T?> GetById(int id);
    Task<List<T>> GetAll();
    T Add(T entity);
    void Update(T entity);
    void Deletar(T entity);
}