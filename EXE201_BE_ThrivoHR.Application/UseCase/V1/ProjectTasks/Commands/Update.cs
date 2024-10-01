using EXE201_BE_ThrivoHR.Application.Common.Security;
using EXE201_BE_ThrivoHR.Domain.Common.Exceptions;

namespace EXE201_BE_ThrivoHR.Application.UseCase.V1.ProjectTasks.Commands;
[Authorize]
public record Update(string TaskId, string? Name, string? Description) : ICommand;
internal sealed class UpdateHandler(IProjectTaskRepository projectTaskRepository) : ICommandHandler<Update>
{
    public async Task<Result> Handle(Update request, CancellationToken cancellationToken)
    {
        var task = await projectTaskRepository.FindAsync(x => x.Id == request.TaskId, cancellationToken)?? throw new NotFoundException("Task not found");
        task.Name = request.Name;
        task.Description = request.Description;
        await projectTaskRepository.UpdateAsync(task);
        await projectTaskRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
        return Result.Success();


    }
}
