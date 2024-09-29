using EXE201_BE_ThrivoHR.Domain.Entities.Base;
using EXE201_BE_ThrivoHR.Domain.Entities.Identity;
using TaskStatus = EXE201_BE_ThrivoHR.Domain.Common.Status.TaskStatus;

namespace EXE201_BE_ThrivoHR.Domain.Entities
{
    public class TaskHistory : AuditableEntity<string>
    {
        public new string Id { get; set; } = Guid.NewGuid().ToString();

        public string? TaskId { get; set; }
        public virtual ProjectTask? Task { get; set; }

        public string? ChangedById { get; set; }
        public virtual AppUser? ChangedBy { get; set; }

        public DateTime ChangeDate { get; set; }

        public TaskStatus? OldStatus { get; set; }
        public TaskStatus? NewStatus { get; set; }

        public string? OldAssigneeId { get; set; }
        public string? NewAssigneeId { get; set; }

        public virtual AppUser? OldAssignee { get; set; }
        public virtual AppUser? NewAssignee { get; set; }

        public DateTime? OldDueDate { get; set; }
        public DateTime? NewDueDate { get; set; }

        public double? OldProgress { get; set; }
        public double? NewProgress { get; set; }

        public string? ChangeDescription { get; set; }
    }
}