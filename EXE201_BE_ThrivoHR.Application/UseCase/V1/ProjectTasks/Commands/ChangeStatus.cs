using EXE201_BE_ThrivoHR.Domain.Common.Exceptions;

namespace EXE201_BE_ThrivoHR.Application.UseCase.V1.ProjectTasks.Commands;

public record ChangeStatus(string TaskId,Domain.Common.Status.TaskStatus Status) : ICommand;
internal sealed class ChangeStatusHandler(IProjectTaskRepository projectTaskRepository) : ICommandHandler<ChangeStatus>
{
    public async Task<Result> Handle(ChangeStatus request, CancellationToken cancellationToken)
    {
        var task = await projectTaskRepository.FindAsync(x => x.Id == request.TaskId, cancellationToken) ?? throw new NotFoundException("Task not found");
        task.Status = request.Status;
        await projectTaskRepository.UpdateAsync(task);
        await projectTaskRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
        return Result.Success();
    }
}
