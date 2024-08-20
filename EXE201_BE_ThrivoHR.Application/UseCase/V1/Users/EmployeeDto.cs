
using EXE201_BE_ThrivoHR.Application.Common.Mappings;
using EXE201_BE_ThrivoHR.Application.Model;
using EXE201_BE_ThrivoHR.Application.UseCase.V1.Addresses;
using EXE201_BE_ThrivoHR.Domain.Entities.Identity;

namespace EXE201_BE_ThrivoHR.Application.UseCase.V1.Users;

public class EmployeeDto : IMapFrom<AppUser>
{
    public string? EmployeeCode { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? FullName { get; set; }
    public string? Email { get; set; }

    public string? IdentityNumber { get; set; }
    public string? PhoneNumber { get; set; }
    public string? TaxCode { get; set; }
    public string? BankAccount { get; set; }
    public AddressDto? Address { get; set; }
    public string? Department { get; set; }
    public string? Position { get; set; }
    public DateOnly? DateOfBirth { get; set; }

    public string? Manager { get; set; }

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
            Address = AddressDto.Create(employeeDto.Address!),
            Department = employeeDto.Department!.Name!,
            Position = employeeDto.Position!.Name,
            DateOfBirth = employeeDto.DateOfBirth,
            EmployeeCode = employeeDto.EmployeeCode,
            Manager = employeeDto.Manager?.FullName,

        };
    }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<AppUser, EmployeeDto>();
        profile.CreateMap<EmployeeModel, AppUser>();


    }
}
