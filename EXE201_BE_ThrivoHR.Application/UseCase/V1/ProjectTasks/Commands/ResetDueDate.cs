using EXE201_BE_ThrivoHR.Domain.Common.Exceptions;

namespace EXE201_BE_ThrivoHR.Application.UseCase.V1.ProjectTasks.Commands;

public record ResetDueDate(string TaskId,DateTime DueDate) : ICommand;
internal sealed class ResetDueDateHandler(IProjectTaskRepository projectTaskRepository) : ICommandHandler<ResetDueDate>
{
    public async Task<Result> Handle(ResetDueDate request, CancellationToken cancellationToken)
    {
        var task = await projectTaskRepository.FindAsync(x => x.Id == request.TaskId, cancellationToken) ?? throw new NotFoundException("Task not found");
        task.DueDate = request.DueDate;
        await projectTaskRepository.UpdateAsync(task);
        await projectTaskRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
        return Result.Success();
    }
}
