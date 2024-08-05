
using EXE201_BE_ThrivoHR.Application.Common.Mappings;
using EXE201_BE_ThrivoHR.Domain.Entities.Identity;

namespace EXE201_BE_ThrivoHR.Application.UseCase.V1.Users;

public class EmployeeDto : IMapFrom<AppUser>
{
    public string EmploeeyCode { get; set; }
    public string Email { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string FullName { get; set; }
    public string IdentityNumber { get; set; }
    public string PhoneNumber { get; set; }
    public string TaxCode { get; set; }
    public string BankAccount { get; set; }
    //  public Address AddressId { get; set; }
    //  public Department DepartmentId { get; set; }
    //  public Position PositionId { get; set; }
    public DateOnly? DateOfBirth { get; set; }

    public static EmployeeDto Create(AppUser employeeDto)
    {
        return new EmployeeDto
        {
            Email = employeeDto.Email,
            FirstName = employeeDto.FirstName,
            LastName = employeeDto.LastName,
            FullName = employeeDto.FullName,
            IdentityNumber = employeeDto.IdentityNumber,
            PhoneNumber = employeeDto.PhoneNumber,
            TaxCode = employeeDto.TaxCode,
            BankAccount = employeeDto.BankAccount,
            //  AddressId = employeeDto.Address,
            // DepartmentId = employeeDto.Department,
            //  PositionId = employeeDto.Position,
            DateOfBirth = employeeDto.DateOfBirth,
            EmploeeyCode = employeeDto.EmploeeyCode

        };
    }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<AppUser, EmployeeDto>();
    }
}
