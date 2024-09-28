using EXE201_BE_ThrivoHR.Domain.Common.Exceptions;
using EXE201_BE_ThrivoHR.Domain.Common.Status;

namespace EXE201_BE_ThrivoHR.Application.UseCase.V1.RewardAndDisciplinarys.Commands;

public record ChangeStatus(string Id, FormStatus FormStatus) : ICommand;
internal sealed class ChangeStatusHandler(IRewardAndDisciplinaryRepository rewardAndDisciplinaryRepository) : ICommandHandler<ChangeStatus>
{
    public async Task<Result> Handle(ChangeStatus request, CancellationToken cancellationToken)
    {
        var rewardAndDisciplinary = await rewardAndDisciplinaryRepository.FindAsync(x => x.Id == request.Id, cancellationToken) ?? throw new NotFoundException("Reward And Disciplinary Not Found");
        rewardAndDisciplinary.Status = request.FormStatus;
        await rewardAndDisciplinaryRepository.UpdateAsync(rewardAndDisciplinary);
        await rewardAndDisciplinaryRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
        return Result.Success();
    }
}
