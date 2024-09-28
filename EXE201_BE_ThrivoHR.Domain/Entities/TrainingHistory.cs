using EXE201_BE_ThrivoHR.Domain.Entities.Base;
using EXE201_BE_ThrivoHR.Domain.Entities.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace EXE201_BE_ThrivoHR.Domain.Entities;

public class TrainingHistory : AuditableEntity<int>
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public new int Id { get; set; }
    public string? EmployeeId { get; set; }
    public DateOnly StartDay { get; set; } = DateOnly.FromDateTime(DateTime.Now);
    public string? WorkshopName { get; set; }
    public string? Content { get; set; }
    public Domain.Common.Status.TrainingHistory Status { get; set; } = Domain.Common.Status.TrainingHistory.NotStarted;




    public virtual AppUser? Employee { get; set; }

}
