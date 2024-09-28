using EXE201_BE_ThrivoHR.Domain.Common.Status;
using EXE201_BE_ThrivoHR.Domain.Entities.Identity;

namespace EXE201_BE_ThrivoHR.Domain.Entities.Base;

public abstract class BaseForm : AuditableEntity<string>
{
    public new string Id { get; set; } = Guid.NewGuid().ToString();
    public string? EmployeeId { get; set; }
    public DateTime DateTime { get; set; } = DateTime.Now;
    public string? Reason { get; set; }
    public string? ApproverId { get; set; }

    public FormStatus Status { get; set; } = FormStatus.Pending;



    public virtual AppUser? Approver { get; set; }
    public virtual AppUser? Employee { get; set; }
}
