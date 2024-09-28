using EXE201_BE_ThrivoHR.Application.Common.Method;
using EXE201_BE_ThrivoHR.Domain.Common.Exceptions;
using EXE201_BE_ThrivoHR.Domain.Entities;

namespace EXE201_BE_ThrivoHR.Application.UseCase.V1.Projects.Commands;

public record EditMemberInProject(string ProjetId, string EmployeeCode, bool IsRemove) : ICommand;
internal sealed class EditMemberInProjectHandler(IProjectRepository projectRepository, IEmployeeRepository employeeRepository, IEmployeesProjectMappingRepository employeesProjectMappingRepository) : ICommandHandler<EditMemberInProject>
{
    public async Task<Result> Handle(EditMemberInProject request, CancellationToken cancellationToken)
    {
        var project = await projectRepository.FindAsync(x => x.Id == request.ProjetId, cancellationToken) ?? throw new NotFoundException(request.ProjetId);
        var Employee = await employeeRepository.FindAsync(x => x.EmployeeId == EmployeesMethod.ConvertEmployeeCodeToId(request.EmployeeCode), cancellationToken) ?? throw new Common.Exceptions.Employee.NotFoundException(request.EmployeeCode);
        if (request.IsRemove)
        {
            var mapping = await employeesProjectMappingRepository.FindAsync(x => x.ProjectId == project.Id && x.EmployeeId == Employee.Id, cancellationToken) ?? throw new NotFoundException("Employee is not in the project");
            mapping.IsDeleted = true;
            await employeesProjectMappingRepository.UpdateAsync(mapping);
        }
        else
        {
            var mapping = await employeesProjectMappingRepository.FindAsync(x => x.ProjectId == project.Id && x.EmployeeId == Employee.Id, cancellationToken);
            if (mapping != null)
            {
                throw new NotFoundException("Employee already in the project");
            }
            var map = new EmployeesProjectMapping
            {
                EmployeeId = Employee.Id,
                ProjectId = project.Id
            };
            await employeesProjectMappingRepository.AddAsync(map);
        }
        await employeesProjectMappingRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
        return Result.Success();
    }
}
