using EXE201_BE_ThrivoHR.Application.Common.Mappings;
using EXE201_BE_ThrivoHR.Domain.Entities;

namespace EXE201_BE_ThrivoHR.Application.UseCase.V1.Salaries;

public class SalaryDto : IMapFrom<Salary>
{
    public string? EmployeeCode { get; set; }
    public string? EmployeeName { get; set; }
    public DateOnly Date { get; set; }

    public decimal BasicSalary { get; set; }

    public decimal Allowance { get; set; }

    public decimal Bonus { get; set; }

    public decimal SocialInsurance { get; set; }

    public decimal HealthInsurance { get; set; }

    public decimal UnemploymentInsurance { get; set; }

    public decimal TotalIncomeBeforeTax { get; set; }

    public decimal PersonalDeduction { get; set; }
    public int NumberOfDependants { get; set; }

    public decimal TaxableIncome { get; set; }

    public decimal PersonalIncomeTax { get; set; }

    public decimal? OtherDeductions { get; set; }

    public decimal NetIncome { get; set; }
    public static SalaryDto Create(Salary salary, IMapper mapper)
        => mapper.Map<SalaryDto>(salary);
    public void Mapping(Profile profile)
    {
        profile.CreateMap<Salary, SalaryDto>()
            .ForMember(d => d.EmployeeCode, opt => opt.MapFrom(s => s.Employee!.EmployeeCode))
            .ForMember(d => d.EmployeeName, opt => opt.MapFrom(s => s.Employee!.FullName))
            .ForMember(d => d.BasicSalary, opt => opt.MapFrom(s => s.Contract!.Salary))
            .ReverseMap();
    }
}
