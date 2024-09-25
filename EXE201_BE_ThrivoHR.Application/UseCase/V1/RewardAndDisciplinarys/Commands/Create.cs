using EXE201_BE_ThrivoHR.Application.Common.Exceptions;
using EXE201_BE_ThrivoHR.Application.Common.Method;
using EXE201_BE_ThrivoHR.Application.Model;
using EXE201_BE_ThrivoHR.Domain.Entities;

namespace EXE201_BE_ThrivoHR.Application.UseCase.V1.RewardAndDisciplinarys.Commands;

public record Create(RewardAndDisciplinaryModel RewardAndDisciplinaryModel) : ICommand;
internal sealed class CreateHandler(IRewardAndDisciplinaryRepository rewardAndDisciplinaryRepository, IMapper mapper, IEmployeeRepository employeeRepository) : ICommandHandler<Create>
{
    public async Task<Result> Handle(Create request, CancellationToken cancellationToken)
    {
        var Employee = await employeeRepository.FindAsync(x => x.EmployeeId == EmployeesMethod.ConvertEmployeeCodeToId(request.RewardAndDisciplinaryModel.EmployeeId!), cancellationToken) ?? throw new Employee.NotFoundException(request.RewardAndDisciplinaryModel.EmployeeId!);
        var model = mapper.Map<RewardsAndDisciplinary>(request.RewardAndDisciplinaryModel);
        model.EmployeeId = Employee.Id;
        await rewardAndDisciplinaryRepository.AddAsync(model);
        await rewardAndDisciplinaryRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
        return Result.Success();
    }
}
