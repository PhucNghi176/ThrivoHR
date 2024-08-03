using Microsoft.AspNetCore.Identity;

namespace EXE201_BE_ThrivoHR.Domain.Entities.Identity;

public class AppUser : IdentityUser<string>
{
    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string FullName { get; set; }

    public DateTime? DayOfBirth { get; set; }

    public bool IsDirector { get; set; }

    public bool IsHeadOfDepartment { get; set; }

    public string? ManagerId { get; set; }

    public string PositionId { get; set; }

    public int IsReceipient { get; set; }

    public virtual ICollection<IdentityUserClaim<string>> Claims { get; set; }
    public virtual ICollection<IdentityUserLogin<string>> Logins { get; set; }
    public virtual ICollection<IdentityUserToken<string>> Tokens { get; set; }
    public virtual ICollection<IdentityUserRole<string>> UserRoles { get; set; }
}
