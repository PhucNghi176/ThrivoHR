using EXE201_BE_ThrivoHR.Domain.Entities.Base;
using EXE201_BE_ThrivoHR.Domain.Entities.Identity;
using TaskStatus = EXE201_BE_ThrivoHR.Domain.Common.Status.TaskStatus;
namespace EXE201_BE_ThrivoHR.Domain.Entities;

public class ProjectTask : AuditableEntity<string>
{
    public new string Id { get; set; } = Guid.NewGuid().ToString();
    public string? Name { get; set; }
    public string? Description { get; set; }
    public string? ProjectId { get; set; }
    public string? AssigneeId { get; set; }
    public DateTime? DueDate { get; set; }
    public double Progress { get; set; }
    public TaskStatus Status { get; set; }
    public virtual Project? Project { get; set; }
    public virtual AppUser? Assignee { get; set; }

}
