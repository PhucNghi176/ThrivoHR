using EXE201_BE_ThrivoHR.Domain.Entities.Base.Contract;

namespace EXE201_BE_ThrivoHR.Domain.Entities.Base;


public abstract class AuditableEntity : AuditableEntity<string>
{
}
public abstract class AuditableEntity<T> : BaseEntity<T>, IAuditableEntity, ISoftDelete
{
    public string? CreatedBy { get; set; }
    public DateTimeOffset CreatedOn { get; }
    public string? LastModifiedBy { get; set; }
    public DateTimeOffset? LastModifiedOn { get; set; }
    public DateTimeOffset? DeletedOn { get; set; }
    public string? DeletedBy { get; set; }

    protected AuditableEntity()
    {
        CreatedOn = DateTimeOffset.UtcNow;
        LastModifiedOn = DateTimeOffset.UtcNow;
    }
}

