using EXE201_BE_ThrivoHR.Domain.Entities.Base;
using EXE201_BE_ThrivoHR.Domain.Entities.Identity;

namespace EXE201_BE_ThrivoHR.Domain.Entities;
public class Attendance : AuditableEntity<string>
{
    public new string Id { get; set; }= Guid.NewGuid().ToString("N");
    public string EmployeeId { get; set; }
    public DateOnly Date { get; set; }
    public TimeOnly? CheckIn { get; set; }
    public TimeOnly? CheckOut { get; set; }
    public string? Note { get; set; }
    
    
    public virtual AppUser Employee { get; set; }
}