using EXE201_BE_ThrivoHR.Domain.Entities.Base;
using EXE201_BE_ThrivoHR.Domain.Entities.Identity;

namespace EXE201_BE_ThrivoHR.Domain.Entities;

public abstract class BaseContract : AuditableEntity<string>
{
    public new string Id { get; set; } = Guid.NewGuid().ToString();
    public required string EmployeeId { get; set; }
    public int? DepartmentId { get; set; }
    public int? PositionId { get; set; }

    public DateOnly StartDate { get; set; }
    public DateOnly? EndDate { get; set; }
    public int? Duration { get; set; }
    public string? Notes { get; set; }

    public bool IsNoExpiry { get; set; } = false;

    public bool IsEnded { get; set; } = false;

    public virtual AppUser? Employee { get; set; }
    public virtual Department? Department { get; set; }
    public virtual Position? Position { get; set; }


}
