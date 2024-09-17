using EXE201_BE_ThrivoHR.Domain.Entities.Base;
using EXE201_BE_ThrivoHR.Domain.Entities.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace EXE201_BE_ThrivoHR.Domain.Entities;

public class Union : AuditableEntity<int>
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public new int Id { get; set; }
    public string? EmployeeId { get; set; }
    public string? Title { get; set; }
    public DateOnly DateJoined { get; set; } = DateOnly.FromDateTime(DateTime.Now);
    public virtual AppUser? Employee { get; set; }
}
