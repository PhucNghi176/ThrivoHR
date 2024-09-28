using EXE201_BE_ThrivoHR.Domain.Common.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EXE201_BE_ThrivoHR.Application.UseCase.V1.ProjectTasks.Commands;

public record ChangeProgress (string TaskId, float Progress) : ICommand;
internal sealed class ChangeProgressHandler(IProjectTaskRepository projectTaskRepository) : ICommandHandler<ChangeProgress>
{
    public async Task<Result> Handle(ChangeProgress request, CancellationToken cancellationToken)
    {
        var task = await projectTaskRepository.FindAsync(x => x.Id == request.TaskId, cancellationToken) ?? throw new NotFoundException("Task not found");
        task.Progress = request.Progress;
        await projectTaskRepository.UpdateAsync(task);
        await projectTaskRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
        return Result.Success();
    }
}

