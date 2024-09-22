using EXE201_BE_ThrivoHR.Domain.Entities.Base;
using EXE201_BE_ThrivoHR.Domain.Entities.Base.Contract;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace EXE201_BE_ThrivoHR.Domain.Entities.Identity;

public class AppUser : IdentityUser<string>, IAuditableEntity
{
    public override string Id { get; set; } = Guid.NewGuid().ToString("N");
    // make coloumn identity 
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int EmployeeId { get; set; }
    public string EmployeeCode
    {
        get { return EmployeeId.ToString("D6"); }
    }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string FullName { get; set; }
    public required string IdentityNumber { get; set; }
    public required bool Sex { get; set; }
    public required string Religion { get; set; }
    public required string Ethnicity { get; set; }
    public required DateOnly? DateOfBirth { get; set; }
    public override string? PhoneNumber { get; set; }
    public required string TaxCode { get; set; }
    public int? AddressId { get; set; }
    public int? DepartmentId { get; set; }
    [ForeignKey("DepartmentId")]
    public int? PositionId { get; set; }
    [ForeignKey("PositionId")]
    public required string BankAccount { get; set; }
    public string? CreatedBy { get; set; }
    public DateTime? CreatedOn { get; set; } = DateTime.UtcNow.AddHours(7);
    public string? LastModifiedBy { get; set; }
    public DateTime? LastModifiedOn { get; set; } = DateTime.UtcNow.AddHours(7);
    public DateTime? DeletedOn { get; set; }
    public string? DeletedBy { get; set; }
    public int NumberOfLeave { get; set; } = 0;

    public bool IsDeleted { get; set; }
    public string? ManagerId { get; set; }
    public string? RefreshToken { get; set; }
    public DateTime? RefreshTokenExpiryTime { get; set; }

    public virtual ICollection<IdentityUserClaim<string>>? Claims { get; set; }
    public virtual ICollection<IdentityUserLogin<string>>? Logins { get; set; }
    public virtual ICollection<IdentityUserToken<string>>? Tokens { get; set; }
    public virtual ICollection<IdentityUserRole<string>>? UserRoles { get; set; }

    public virtual ICollection<BaseContract>? Contracts { get; set; }
    public virtual Department? Department { get; set; }
    public virtual Position? Position { get; set; }
    public virtual Address? Address { get; set; }
    public virtual AppUser? Manager { get; set; }

}
