using EXE201_BE_ThrivoHR.Domain.Common.Exceptions;

namespace EXE201_BE_ThrivoHR.Application.UseCase.V1.ProjectTasks.Commands;

public record Delete(string TaskId) : ICommand;
internal sealed class DeleteHandler(IProjectTaskRepository projectTaskRepository) : ICommandHandler<Delete>
{
    public async Task<Result> Handle(Delete request, CancellationToken cancellationToken)
    {
        var task = await projectTaskRepository.FindAsync(x => x.Id == request.TaskId, cancellationToken) ?? throw new NotFoundException("Task not found");
        task.IsDeleted = true;
        await projectTaskRepository.UpdateAsync(task);
        await projectTaskRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
        return Result.Success();
    }
}
