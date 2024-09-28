using EXE201_BE_ThrivoHR.Domain.Entities.Base;
using EXE201_BE_ThrivoHR.Domain.Entities.Identity;
using TaskStatus = EXE201_BE_ThrivoHR.Domain.Common.Status.TaskStatus;
namespace EXE201_BE_ThrivoHR.Domain.Entities;

public class Project : AuditableEntity<string>
{
    public new string Id { get; set; } = Guid.NewGuid().ToString();
    public string? Name { get; set; }
    public string? Description { get; set; }
    public string? LeaderId { get; set; }
    public ICollection<AppUser> Employees { get; set; } = [];
    public ICollection<ProjectTask> Tasks { get; set; } = [];
    public string? SubLeaderId { get; set; }
    public double Progress { get; set; }
    public virtual AppUser? Leader { get; set; }
    public virtual AppUser? SubLeader { get; set; }
    public TaskStatus Status { get; set; }
}
