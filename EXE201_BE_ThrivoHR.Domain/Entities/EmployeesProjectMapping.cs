using EXE201_BE_ThrivoHR.Domain.Entities.Base.Contract;
using EXE201_BE_ThrivoHR.Domain.Entities.Identity;

namespace EXE201_BE_ThrivoHR.Domain.Entities;

public class EmployeesProjectMapping : IAuditableEntity
{
    public string? EmployeeId { get; set; }
    public string? ProjectId { get; set; }
    public virtual AppUser? Employee { get; set; }
    public virtual Project? Project { get; set; }
    public string? CreatedBy { get ; set ; }
    public DateTime? CreatedOn { get ; set ; } =DateTime.UtcNow.AddHours(7);
    public string? LastModifiedBy { get ; set ; }
    public DateTime? LastModifiedOn { get ; set ; }
    public DateTime? DeletedOn { get ; set ; }
    public string? DeletedBy { get ; set ; }
    public bool IsDeleted { get ; set ; }
}
