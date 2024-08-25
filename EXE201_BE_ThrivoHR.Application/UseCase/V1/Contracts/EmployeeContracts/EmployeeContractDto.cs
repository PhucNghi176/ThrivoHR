using EXE201_BE_ThrivoHR.Application.Common.Mappings;
using EXE201_BE_ThrivoHR.Application.Model;
using EXE201_BE_ThrivoHR.Domain.Entities.Contracts;

namespace EXE201_BE_ThrivoHR.Application.UseCase.V1.Contracts.EmployeeContracts;

public class EmployeeContractDto : IMapFrom<EmployeeContract>
{
    public string? EmployeeCode { get; set; }
    public string? Department { get; set; }
    public string? Position { get; set; }
    public DateOnly StartDate { get; set; }
    public DateOnly? EndDate { get; set; }
    public string? Notes { get; set; }

    public int? Duration { get; set; }
    public bool IsNoExpiry { get; set; }

    public decimal Salary { get; set; }

    public static EmployeeContractDto Create(EmployeeContract employeeContract)
    {
        return new EmployeeContractDto
        {
            EmployeeCode = employeeContract.Employee!.EmployeeCode,
            Department = employeeContract.Department!.Name,
            Position = employeeContract.Position!.Name,
            StartDate = employeeContract.StartDate,
            EndDate = employeeContract.EndDate,
            Notes = employeeContract.Notes,
            Salary = employeeContract.Salary,
            Duration = employeeContract.Duration,
            IsNoExpiry = employeeContract.IsNoExpiry
        };
    }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<EmployeeContract, EmployeeContractDto>()
             .ForMember(x => x.Position, o => o.MapFrom(src => src.Position != null ? src.Position.Name : null))
            .ForMember(x => x.Department, o => o.MapFrom(src => src.Department != null ? src.Department.Name : null))
            .ForMember(x => x.EmployeeCode, o => o.MapFrom(src => src.Employee != null ? src.Employee.EmployeeCode : null));
        profile.CreateMap<EmployeeContractBase, EmployeeContract>();

    }
}

