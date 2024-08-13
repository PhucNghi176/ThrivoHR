namespace EXE201_BE_ThrivoHR.Domain.Entities.Identity;

public class Function
{
    public required string Id { get; set; }
    public required string Name { get; set; }
    public required string Url { get; set; }
    public required string ParrentId { get; set; }
    public required int? SortOrder { get; set; }
    public required string CssClass { get; set; }
    public required bool? IsActive { get; set; }

    public virtual ICollection<Permission>? Permissions { get; set; }
    public virtual ICollection<ActionInFunction>? ActionInFunctions { get; set; }
}
