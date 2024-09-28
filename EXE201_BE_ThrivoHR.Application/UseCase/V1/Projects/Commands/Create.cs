using EXE201_BE_ThrivoHR.Application.Common.Method;
using EXE201_BE_ThrivoHR.Application.Model;
using EXE201_BE_ThrivoHR.Domain.Entities;

namespace EXE201_BE_ThrivoHR.Application.UseCase.V1.Projects.Commands;

public record Create(ProjectModel ProjectModel) : ICommand;
internal sealed class CreateHandler(IProjectRepository projectRepository, IMapper mapper, IEmployeeRepository employeeRepository, IEmployeesProjectMappingRepository employeesProjectMappingRepository) : ICommandHandler<Create>
{
    public async Task<Result> Handle(Create request, CancellationToken cancellationToken)
    {
        var project = mapper.Map<Project>(request.ProjectModel);
        var leader = await employeeRepository.FindAsync(x => x.EmployeeId == EmployeesMethod.ConvertEmployeeCodeToId(request.ProjectModel.LeaderCode), cancellationToken) ?? throw new Common.Exceptions.Employee.NotFoundException(request.ProjectModel.LeaderCode);
        project.LeaderId = leader.Id;
        var mapping = new EmployeesProjectMapping
        {
            EmployeeId = leader.Id,
            ProjectId = project.Id
        };
        await employeesProjectMappingRepository.AddAsync(mapping);
        if (!string.IsNullOrEmpty(request.ProjectModel.SubLeaderCode))
        {
            var subLeader = await employeeRepository.FindAsync(x => x.EmployeeId == EmployeesMethod.ConvertEmployeeCodeToId(request.ProjectModel.SubLeaderCode), cancellationToken) ?? throw new Common.Exceptions.Employee.NotFoundException(request.ProjectModel.SubLeaderCode);
            project.SubLeaderId = subLeader.Id;
            var subLeaderMapping = new EmployeesProjectMapping
            {
                EmployeeId = subLeader.Id,
                ProjectId = project.Id
            };
            await employeesProjectMappingRepository.AddAsync(subLeaderMapping);
        }
        await projectRepository.AddAsync(project);
        await projectRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
        return Result.Success();
    }
}
