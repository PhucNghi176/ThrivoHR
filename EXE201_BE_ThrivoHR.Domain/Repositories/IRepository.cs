namespace EXE201_BE_ThrivoHR.Domain.Repositories;

public interface IRepository<in TDomain>
{
    Task AddAsync(TDomain entity);
    Task UpdateAsync(TDomain entity);
    Task RemoveAsync(TDomain entity);
    Task AddRangeAsync(IEnumerable<TDomain> entities);

}
