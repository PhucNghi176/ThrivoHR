using EXE201_BE_ThrivoHR.Application.Common.Mappings;
using EXE201_BE_ThrivoHR.Application.Model;
using EXE201_BE_ThrivoHR.Domain.Entities.Contracts;

namespace EXE201_BE_ThrivoHR.Application.UseCase.V1.Contracts;

public class EmployeeContractDto : IMapFrom<EmployeeContract>
{
    public string? EmployeeCode { get; set; }
    public string? Department { get; set; }
    public string? Position { get; set; }
    public DateOnly StartDate { get; set; }
    public DateOnly? EndDate { get; set; }
    public string? Notes { get; set; }

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
            Salary = employeeContract.Salary
        };
    }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<EmployeeContract, EmployeeContractDto>();
        profile.CreateMap<EmployeeContractModel, EmployeeContract>()
            .ForMember(x => x.Employee, o => o.Ignore());
    }
}

