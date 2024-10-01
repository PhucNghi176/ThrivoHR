
using EXE201_BE_ThrivoHR.Application.Common.Method;
using EXE201_BE_ThrivoHR.Domain.Entities;

namespace EXE201_BE_ThrivoHR.Application.UseCase.V1.Salaries.Commands;

public record GeneratePayRoll : ICommand;
internal sealed class GeneratePayRollHandler(IEmployeeRepository employeeRepository, IEmployeeContractRepository employeeContractRepository, ISalaryRepository salaryRepository, ISystemConfigRepository systemConfigRepository) : ICommandHandler<GeneratePayRoll>
{
    public async Task<Result> Handle(GeneratePayRoll request, CancellationToken cancellationToken)
    {
        var employees = await employeeRepository.FindAllAsync(cancellationToken);
        var systemConfig = await systemConfigRepository.FindAllAsync(cancellationToken);
        var config = systemConfig.ToDictionary(c => c.Key, c => c.Value);

        var socialInsuranceRate = config["SocialInsurance"];
        var healthInsuranceRate = config["HealthInsurance"];
        var unemploymentInsuranceRate = config["UnemploymentInsurance"];
        var personalDeduction = config["PersonalDeduction"];
        var basicSalary = config["BasicSalary"];
        var MinimumSalaryRegion = config["MinimumSalaryRegion1"];
        var salaries = new List<Salary>();
        foreach (var employee in employees)
        {
            var contract = await employeeContractRepository.FindAsync(x => x.EmployeeId == employee.Id, cancellationToken);
            if (contract == null) continue;

            var totalIncome = contract.Salary;
            decimal socialInsurance;
            decimal healthInsurance;
            decimal unemploymentInsurance;
            var MaxSalary = 20 * basicSalary;
            var MinumuSalary = MinimumSalaryRegion*20;
            if (totalIncome > MaxSalary)
            {
                socialInsurance = MaxSalary * socialInsuranceRate;
                healthInsurance = MaxSalary * healthInsuranceRate;
               // unemploymentInsurance = MaxSalary * unemploymentInsuranceRate;
            }
            else
            {
                socialInsurance = totalIncome * socialInsuranceRate;
                healthInsurance = totalIncome * healthInsuranceRate;
              //  unemploymentInsurance = totalIncome * unemploymentInsuranceRate;
            }
            if(totalIncome > MinumuSalary)
            {
                unemploymentInsurance= MinumuSalary * unemploymentInsuranceRate;
            }
            else
            {
                unemploymentInsurance = totalIncome * unemploymentInsuranceRate;
            }
            var totalIncomeBeforeTax = totalIncome - socialInsurance - healthInsurance - unemploymentInsurance;
            var taxableIncome = totalIncomeBeforeTax - personalDeduction;
            decimal personalIncomeTax;
            decimal netIncome;
            if (taxableIncome <= 0)
            {
                personalIncomeTax = totalIncomeBeforeTax;
                netIncome = personalIncomeTax;
                taxableIncome = 0;
            }
            else
            {
                personalIncomeTax = SalaryMethod.CalculatePersonalIncomeTax(taxableIncome, config);
                netIncome = totalIncomeBeforeTax - personalIncomeTax;

            }

            //taxableIncome - personalIncomeTax;

            var salary = new Salary
            {
                EmployeeId = employee.Id,
                ContractId = contract.Id,
                Date = DateOnly.FromDateTime(DateTime.Now),
                BasicSalary = contract.Salary,
                TotalAllowance = 0,
                Bonus = 0,
                TotalIncome = totalIncome,
                SocialInsurance = socialInsurance,
                HealthInsurance = healthInsurance,
                UnemploymentInsurance = unemploymentInsurance,
                TotalIncomeBeforeTax = totalIncomeBeforeTax,
                PersonalDeduction = personalDeduction,
                NumberOfDependants = 0,
                TaxableIncome = taxableIncome,
                PersonalIncomeTax = personalIncomeTax,
                OtherDeductions = 0,
                NetIncome = netIncome
            };

            salaries.Add(salary);
        }

        await salaryRepository.AddRangeAsync(salaries);
        await salaryRepository.UnitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();


    }
}
