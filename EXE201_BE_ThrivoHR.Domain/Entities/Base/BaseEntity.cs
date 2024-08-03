using EXE201_BE_ThrivoHR.Domain.Entities.Base.Contract;

namespace EXE201_BE_ThrivoHR.Domain.Entities.Base
{
    public abstract class BaseEntity<TId> : IEntity<TId>
    {
        public TId ID { get; protected set; } = default!;
    }
    public abstract class BaseEntity : BaseEntity<string>
    {
        protected BaseEntity()
        {
            ID = Guid.NewGuid().ToString("N");

        }
    }
}
