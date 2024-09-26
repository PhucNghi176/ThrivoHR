using EXE201_BE_ThrivoHR.Application.Model;
using EXE201_BE_ThrivoHR.Domain.Entities;

namespace EXE201_BE_ThrivoHR.Application.UseCase.V1.Overtimes.Commands;

public record CreateOvertime(OvertimeModel OvertimeModel) : ICommand;
internal sealed class CreateOvertimeHandler(IOvertimeRepository overtimeRepository, IMapper mapper) : ICommandHandler<CreateOvertime>
{
    public async Task<Result> Handle(CreateOvertime request, CancellationToken cancellationToken)
    {
        await overtimeRepository.AddAsync(mapper.Map<Overtime>(request.OvertimeModel));
        await overtimeRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
        return Result.Success();
    }
}

