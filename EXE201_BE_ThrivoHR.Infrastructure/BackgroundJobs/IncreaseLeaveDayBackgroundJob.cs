using EXE201_BE_ThrivoHR.Domain.Repositories;
using Quartz;

namespace EXE201_BE_ThrivoHR.Infrastructure.BackgroundJobs;

public class IncreaseLeaveDayBackgroundJob : IJob
{
    private readonly IEmployeeRepository _employeeRepository;

    public IncreaseLeaveDayBackgroundJob(IEmployeeRepository employeeRepository)
    {
        _employeeRepository = employeeRepository;
    }

    public async Task Execute(IJobExecutionContext context)
    {
        var employees = await _employeeRepository.FindAsync(x=>x.EmployeeId==1);
        //foreach (var employee in employees)
        //{
        //    employee.NumberOfLeave += 1;
        //    await _employeeRepository.UpdateAsync(employee);
        //}
        employees.NumberOfLeave += 1;
        await _employeeRepository.UpdateAsync(employees);
        await _employeeRepository.UnitOfWork.SaveChangesAsync();

    }
}
