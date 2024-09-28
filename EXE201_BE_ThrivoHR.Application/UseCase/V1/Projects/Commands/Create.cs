using EXE201_BE_ThrivoHR.Application.Common.Method;
using EXE201_BE_ThrivoHR.Application.Model;
using EXE201_BE_ThrivoHR.Domain.Entities;

namespace EXE201_BE_ThrivoHR.Application.UseCase.V1.Projects.Commands;

public record Create(ProjectModel ProjectModel) : ICommand;
internal sealed class CreateHandler(IProjectRepository projectRepository, IMapper mapper, IEmployeeRepository employeeRepository) : ICommandHandler<Create>
{
    public async Task<Result> Handle(Create request, CancellationToken cancellationToken)
    {
        var project = mapper.Map<Project>(request.ProjectModel);
        var leader = await employeeRepository.FindAsync(x => x.EmployeeId == EmployeesMethod.ConvertEmployeeCodeToId(request.ProjectModel.LeaderCode), cancellationToken) ?? throw new Common.Exceptions.Employee.NotFoundException(request.ProjectModel.LeaderCode);
        project.LeaderId = leader.Id;
        if (request.ProjectModel.SubLeaderCode != null)
        {
            var subLeader = await employeeRepository.FindAsync(x => x.EmployeeId == EmployeesMethod.ConvertEmployeeCodeToId(request.ProjectModel.SubLeaderCode), cancellationToken);
            project.SubLeaderId = subLeader.Id;
        }
        await projectRepository.AddAsync(project);
        await projectRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
        return Result.Success();
    }
}
