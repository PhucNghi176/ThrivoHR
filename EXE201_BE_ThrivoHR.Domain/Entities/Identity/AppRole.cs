using Microsoft.AspNetCore.Identity;

namespace EXE201_BE_ThrivoHR.Domain.Entities.Identity;

public class AppRole : IdentityRole<string>
{
    public required string Description { get; set; }
    public required string RoleCode { get; set; }

    public virtual ICollection<IdentityUserRole<string>>? UserRoles { get; set; }
    public virtual ICollection<IdentityRoleClaim<string>>? Claims { get; set; }
    public virtual ICollection<Permission>? Permissions { get; set; }
}
