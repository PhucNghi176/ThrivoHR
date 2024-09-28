using EXE201_BE_ThrivoHR.Application.Common.Exceptions;
using EXE201_BE_ThrivoHR.Application.Model;
using EXE201_BE_ThrivoHR.Domain.Entities;

namespace EXE201_BE_ThrivoHR.Application.UseCase.V1.ProjectTasks.Commands;

public record Create(ProjectTaskModel ProjectTaskModel) : ICommand;
internal sealed class CreateHandler(IProjectTaskRepository projectTaskRepository, IMapper mapper, IEmployeeRepository employeeRepository) : ICommandHandler<Create>
{
    public async Task<Result> Handle(Create request, CancellationToken cancellationToken)
    {
        var E = await employeeRepository.FindByEmployeeCode(request.ProjectTaskModel.AssigneeCode!, cancellationToken) ?? throw new Employee.NotFoundException(request.ProjectTaskModel.AssigneeCode);
        var P = mapper.Map<ProjectTask>(request.ProjectTaskModel);
        P.AssigneeId = E.Id;
        await projectTaskRepository.AddAsync(P);
        await projectTaskRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
        return Result.Success();
    }
}

