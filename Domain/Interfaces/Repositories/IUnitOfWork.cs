namespace ImpressioApi_.Domain.Interfaces.Repositories;

public interface IUnitOfWork
{
    Task<bool> Commit();
}