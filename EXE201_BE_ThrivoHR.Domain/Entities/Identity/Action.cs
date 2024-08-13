namespace EXE201_BE_ThrivoHR.Domain.Entities.Identity;

public class Action
{
    public required string Id { get; set; }
    public required string Name { get; set; }
    public int? SortOrder { get; set; }
    public bool? IsActive { get; set; }

    public virtual ICollection<Permission>? Permissions { get; set; }
    public virtual ICollection<ActionInFunction>? ActionInFunctions { get; set; }
}
