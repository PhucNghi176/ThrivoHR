using EXE201_BE_ThrivoHR.Domain.Entities.Base;
using EXE201_BE_ThrivoHR.Domain.Entities.Identity;

namespace EXE201_BE_ThrivoHR.Domain.Entities;

public abstract class BaseContract : AuditableEntity<string>
{
    public string? EmployeeId { get; set; }
    public int? DepartmentId { get; set; }
    public int? PositionId { get; set; }

    public DateTime StartDate { get { return CreatedOn!.Value; } }
    public DateTime? EndDate { get; set; }
    public int? Duration { get; set; }






    public virtual AppUser? Employee { get; set; }
    public virtual Department? Department { get; set; }
    public virtual Position? Position { get; set; }


}
