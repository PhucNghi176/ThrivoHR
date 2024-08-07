using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EXE201_BE_ThrivoHR.Domain.Entities.Identity;

public class AppUser : IdentityUser<string>
{
    public override string Id { get; set; } = Guid.NewGuid().ToString("N");
    // make coloumn identity 
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int EmployeeId { get; set; }
    public string EmployeeCode
    {
        get { return EmployeeId.ToString("D6"); }
    }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string FullName { get; set; }
    public string IdentityNumber { get; set; }
    public DateOnly? DateOfBirth { get; set; }
    public string PhoneNumber { get; set; }
    public string TaxCode { get; set; }
    public int? AddressId { get; set; }
    public int? DepartmentId { get; set; }
    [ForeignKey("DepartmentId")]
    public int? PositionId { get; set; }
    [ForeignKey("PositionId")]
    public string BankAccount { get; set; }
    public string? CreatedBy { get; set; }
    public DateTime? CreatedOn { get; set; } = DateTime.UtcNow.AddHours(7);
    public string? LastModifiedBy { get; set; }
    public DateTime? LastModifiedOn { get; set; } = DateTime.UtcNow.AddHours(7);
    public DateTime? DeletedOn { get; set; }
    public string? DeletedBy { get; set; }

    public string? RefreshToken { get; set; }
    public DateTime? RefreshTokenExpiryTime { get; set; }

    public virtual ICollection<IdentityUserClaim<string>> Claims { get; set; }
    public virtual ICollection<IdentityUserLogin<string>> Logins { get; set; }
    public virtual ICollection<IdentityUserToken<string>> Tokens { get; set; }
    public virtual ICollection<IdentityUserRole<string>> UserRoles { get; set; }
    public virtual Department? Department { get; set; }
    public virtual Position? Position { get; set; }
    public virtual Address? Address { get; set; }
}
