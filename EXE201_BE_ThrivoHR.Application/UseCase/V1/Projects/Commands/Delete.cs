using EXE201_BE_ThrivoHR.Domain.Common.Exceptions;

namespace EXE201_BE_ThrivoHR.Application.UseCase.V1.Projects.Commands;

public record Delete(string ProjectId) : ICommand;
internal sealed class DeleteHandler(IProjectRepository projectRepository, IEmployeesProjectMappingRepository employeesProjectMappingRepository) : ICommandHandler<Delete>
{
    public async Task<Result> Handle(Delete request, CancellationToken cancellationToken)
    {
        var project = await projectRepository.FindAsync(x => x.Id == request.ProjectId, cancellationToken) ?? throw new NotFoundException(request.ProjectId);
        project.IsDeleted = true;
        await projectRepository.UpdateAsync(project);
        var mappings = await employeesProjectMappingRepository.FindAllAsync(x => x.ProjectId == project.Id, cancellationToken);
        foreach (var mapping in mappings)
        {
            mapping.IsDeleted = true;
            await employeesProjectMappingRepository.UpdateAsync(mapping);
        }
        await projectRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
        return Result.Success();
    }
}
