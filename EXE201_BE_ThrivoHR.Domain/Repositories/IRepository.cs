namespace EXE201_BE_ThrivoHR.Domain.Repositories;

public interface IRepository<in TDomain>
{
    Task Add(TDomain entity);
    Task Update(TDomain entity);
    Task Remove(TDomain entity);
}
