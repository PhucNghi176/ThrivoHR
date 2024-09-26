
namespace EXE201_BE_ThrivoHR.Application.UseCase.V1.RewardAndDisciplinarys.Commands;

public record Delete(string Id) : ICommand;
internal sealed class DeleteHandler(IRewardAndDisciplinaryRepository rewardAndDisciplinaryRepository) : ICommandHandler<Delete>
{
    public async Task<Result> Handle(Delete request, CancellationToken cancellationToken)
    {
        await rewardAndDisciplinaryRepository.FindAsync(x => x.Id == request.Id, cancellationToken);
        await rewardAndDisciplinaryRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
        return Result.Success();
    }
}
