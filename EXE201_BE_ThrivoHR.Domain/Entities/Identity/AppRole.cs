using Microsoft.AspNetCore.Identity;

namespace EXE201_BE_ThrivoHR.Domain.Entities.Identity;

public class AppRole : IdentityRole<string>
{
    public string Description { get; set; }
    public string RoleCode { get; set; }

    public virtual ICollection<IdentityUserRole<string>> UserRoles { get; set; }
    public virtual ICollection<IdentityRoleClaim<string>> Claims { get; set; }
    public virtual ICollection<Permission> Permissions { get; set; }
}
