using EXE201_BE_ThrivoHR.Domain.Entities.Base.Contract;

namespace EXE201_BE_ThrivoHR.Domain.Entities.Base;


public abstract class AuditableEntity : AuditableEntity<string>
{
}
public abstract class AuditableEntity<T> : BaseEntity<T>, IAuditableEntity, ISoftDelete
{
    public string? CreatedBy { get; set; }
    public DateTime? CreatedOn { get; set; }
    public string? LastModifiedBy { get; set; }
    public DateTime? LastModifiedOn { get; set; }
    public DateTime? DeletedOn { get; set; }
    public string? DeletedBy { get; set; }
    public bool IsDeleted { get; set; } = false;

    protected AuditableEntity()
    {
        CreatedOn = LastModifiedOn = DateTime.UtcNow.AddHours(7);

    }
}

