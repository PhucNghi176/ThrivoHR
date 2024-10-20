using EXE201_BE_ThrivoHR.Domain.Entities.Base.Contract;

namespace EXE201_BE_ThrivoHR.Domain.Entities.Base;
public abstract class BaseEntity<TId> : IEntity<TId>
{

    public TId Id { get; protected set; } = default!;
}
public abstract class BaseEntity : BaseEntity<string>
{
    protected BaseEntity()
    {
        Id = Guid.NewGuid().ToString("N");

    }
}