using EXE201_BE_ThrivoHR.Application.Common.Mappings;
using EXE201_BE_ThrivoHR.Application.Model;
using EXE201_BE_ThrivoHR.Domain.Entities.Contracts;
using System.Globalization;

namespace EXE201_BE_ThrivoHR.Application.UseCase.V1.Contracts.EmployeeContracts;

public class EmployeeContractDto : IMapFrom<EmployeeContract>
{
    public required string Id { get; set; }
    public string? EmployeeName { get; set; }
    public string? EmployeeCode { get; set; }
    public string? Department { get; set; }
    public string? Position { get; set; }
    public DateOnly StartDate { get; set; }
    public DateOnly? EndDate { get; set; }
    public string? Notes { get; set; }

    public int? Duration { get; set; }
    public bool IsNoExpiry { get; set; }

    // Modify Salary to be a string to hold the formatted value
    public string? Salary { get; set; }

    public static EmployeeContractDto Create(EmployeeContract employeeContract)
    {
        // Set the CultureInfo for VND formatting
        CultureInfo vndCulture = new("vi-VN");

        return new EmployeeContractDto
        {
            Id = employeeContract.Id,
            EmployeeCode = employeeContract.Employee!.EmployeeCode,
            Department = employeeContract.Department!.Name,
            Position = employeeContract.Position!.Name,
            StartDate = employeeContract.StartDate,
            EndDate = employeeContract.EndDate,
            Notes = employeeContract.Notes,

            // Format the Salary as VND currency, assuming Salary is decimal and cannot be null
            Salary = employeeContract.Salary.ToString("C", vndCulture),

            Duration = employeeContract.Duration,
            IsNoExpiry = employeeContract.IsNoExpiry,
            EmployeeName = employeeContract.Employee!.FullName
        };
    }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<EmployeeContract, EmployeeContractDto>()
             .ForMember(x => x.Position, o => o.MapFrom(src => src.Position != null ? src.Position.Name : null))
            .ForMember(x => x.Department, o => o.MapFrom(src => src.Department != null ? src.Department.Name : null))
            .ForMember(x => x.EmployeeCode, o => o.MapFrom(src => src.Employee != null ? src.Employee.EmployeeCode : null))
            .ForMember(x => x.EmployeeName, o => o.MapFrom(src => src.Employee != null ? src.Employee.FullName : null))
            .ForMember(x => x.Salary, o => o.MapFrom(src => src.Salary.ToString("C", new CultureInfo("vi-VN"))))
            ;
        profile.CreateMap<EmployeeContractBase, EmployeeContract>();

    }
}
