using EXE201_BE_ThrivoHR.Domain.Common.Exceptions;

namespace EXE201_BE_ThrivoHR.Application.UseCase.V1.RewardAndDisciplinarys.Commands;

public record Delete(string Id) : ICommand;
internal sealed class DeleteHandler(IRewardAndDisciplinaryRepository rewardAndDisciplinaryRepository) : ICommandHandler<Delete>
{
    public async Task<Result> Handle(Delete request, CancellationToken cancellationToken)
    {
        var re = await rewardAndDisciplinaryRepository.FindAsync(x => x.Id == request.Id, cancellationToken) ?? throw new NotFoundException(request.Id.ToString());
        re.IsDeleted = true;
        await rewardAndDisciplinaryRepository.UpdateAsync(re);
        await rewardAndDisciplinaryRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
        return Result.Success();
    }
}
