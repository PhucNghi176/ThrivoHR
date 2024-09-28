using EXE201_BE_ThrivoHR.Application.Model;
using EXE201_BE_ThrivoHR.Domain.Entities;

namespace EXE201_BE_ThrivoHR.Application.UseCase.V1.ProjectTasks.Commands;

public record Create(ProjectTaskModel ProjectTaskModel) : ICommand;
internal sealed class CreateHandler(IProjectTaskRepository projectTaskRepository, IMapper mapper) : ICommandHandler<Create>
{
    public async Task<Result> Handle(Create request, CancellationToken cancellationToken)
    {
        await projectTaskRepository.AddAsync(mapper.Map<ProjectTask>(request.ProjectTaskModel));
        await projectTaskRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
        return Result.Success();
    }
}
