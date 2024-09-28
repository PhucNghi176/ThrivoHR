using EXE201_BE_ThrivoHR.Domain.Common.Exceptions;

namespace EXE201_BE_ThrivoHR.Application.UseCase.V1.Projects.Commands;

public record ChangeStatus (string Id, Domain.Common.Status.TaskStatus Status) : ICommand;
internal sealed class ChangeStatusHanlder(IProjectRepository projectRepository) : ICommandHandler<ChangeStatus>
{
    public async Task<Result> Handle(ChangeStatus request, CancellationToken cancellationToken)
    {
        var project = await projectRepository.FindAsync(x => x.Id == request.Id, cancellationToken) ?? throw new NotFoundException(request.Id);
        project.Status = request.Status;
        await projectRepository.UpdateAsync(project);
        await projectRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
        return Result.Success();
    }
}
