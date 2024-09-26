using EXE201_BE_ThrivoHR.Domain.Common.Status;
using EXE201_BE_ThrivoHR.Domain.Entities.Base;
using EXE201_BE_ThrivoHR.Domain.Entities.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace EXE201_BE_ThrivoHR.Domain.Entities;

public class Overtime : AuditableEntity<string>
{
    public new string Id { get; set; } = Guid.NewGuid().ToString();
    public string? EmployeeId { get; set; }
    public DateOnly Date { get; set; }
    public int From { get; set; }
    public int To { get; set; }
    public string? Reason { get; set; }
    public FormStatus Status { get; set; } = FormStatus.Pending;
    public bool IsPaid { get; set; } = false;
    [Column(TypeName = "decimal(18,2)")]
    public decimal? Amount { get; set; }
    public string? ApproverId { get; set; }

    public virtual AppUser? Employee { get; set; }
    public virtual AppUser? Approver { get; set; }

}
