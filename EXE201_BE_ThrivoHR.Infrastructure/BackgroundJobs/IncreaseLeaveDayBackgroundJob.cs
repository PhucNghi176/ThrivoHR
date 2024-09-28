using EXE201_BE_ThrivoHR.Domain.Repositories;
using Quartz;

namespace EXE201_BE_ThrivoHR.Infrastructure.BackgroundJobs;

public class IncreaseLeaveDayBackgroundJob(IEmployeeRepository employeeRepository) : IJob
{
    public async Task Execute(IJobExecutionContext context)
    {
        var employees = await employeeRepository.FindAsync(x => x.EmployeeId == 1);
        //foreach (var employee in employees)
        //{
        //    employee.NumberOfLeave += 1;
        //    await _employeeRepository.UpdateAsync(employee);
        //}
        employees.NumberOfLeave += 1;
        await employeeRepository.UpdateAsync(employees);
        await employeeRepository.UnitOfWork.SaveChangesAsync();

    }
}
