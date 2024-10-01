using EXE201_BE_ThrivoHR.Application.Common.Method;
using EXE201_BE_ThrivoHR.Application.Model;
using EXE201_BE_ThrivoHR.Domain.Common.Exceptions;

namespace EXE201_BE_ThrivoHR.Application.UseCase.V1.Projects.Commands;

public record UpdateProject (string Id, ProjectModel ProjectModel) : ICommand;
internal sealed class UpdateProjectHandler(IProjectRepository projectRepository, IEmployeeRepository employeeRepository) : ICommandHandler<UpdateProject>
{
    public async Task<Result> Handle(UpdateProject request, CancellationToken cancellationToken)
    {
        var project = await projectRepository.FindAsync(x => x.Id == request.Id, cancellationToken) ?? throw new NotFoundException(request.Id);
        var leader = await employeeRepository.FindAsync(x => x.EmployeeId == EmployeesMethod.ConvertEmployeeCodeToId(request.ProjectModel.LeaderCode!), cancellationToken) ?? throw new Common.Exceptions.Employee.NotFoundException(request.ProjectModel.LeaderCode!);
        project.LeaderId = leader.Id;
        if (!string.IsNullOrEmpty(request.ProjectModel.SubLeaderCode))
        {
            var subLeader = await employeeRepository.FindAsync(x => x.EmployeeId == EmployeesMethod.ConvertEmployeeCodeToId(request.ProjectModel.SubLeaderCode), cancellationToken)?? throw new Common.Exceptions.Employee.NotFoundException(request.ProjectModel.SubLeaderCode) ;
            project.SubLeaderId = subLeader.Id;
        }
        project.Name = request.ProjectModel.Name;
        project.Description = request.ProjectModel.Description;
        await projectRepository.UpdateAsync(project);
        await projectRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
        return Result.Success();
    }
}
