using EXE201_BE_ThrivoHR.Application.Common.Exceptions;
using EXE201_BE_ThrivoHR.Domain.Common.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EXE201_BE_ThrivoHR.Application.UseCase.V1.ProjectTasks.Commands;

public record ChangeAssignee(string TaskId, string AssigneeCode) : ICommand;
internal sealed class ChangeAssigneeHandler(IProjectTaskRepository projectTaskRepository, IEmployeeRepository employeeRepository) : ICommandHandler<ChangeAssignee>
{
    public async Task<Result> Handle(ChangeAssignee request, CancellationToken cancellationToken)
    {
        var task = await projectTaskRepository.FindAsync(x => x.Id == request.TaskId, cancellationToken) ?? throw new NotFoundException("Task not found");
        var employee = await employeeRepository.FindByEmployeeCode(request.AssigneeCode, cancellationToken) ?? throw new Employee.NotFoundException(request.AssigneeCode);
        task.Id = employee.Id;
        await projectTaskRepository.UpdateAsync(task);
        await projectTaskRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
        return Result.Success();
    }
}
