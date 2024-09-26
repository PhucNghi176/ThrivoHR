using EXE201_BE_ThrivoHR.Application.Model;
using EXE201_BE_ThrivoHR.Domain.Common.Exceptions;

namespace EXE201_BE_ThrivoHR.Application.UseCase.V1.RewardAndDisciplinarys.Commands;

public record Update(RewardAndDisciplinaryModel RewardAndDisciplinaryModel, string Id) : ICommand;
internal sealed class UpdateHandler(IRewardAndDisciplinaryRepository rewardAndDisciplinaryRepository, IMapper mapper) : ICommandHandler<Update>
{
    public async Task<Result> Handle(Update request, CancellationToken cancellationToken)
    {
        var rewardAndDisciplinary = await rewardAndDisciplinaryRepository.FindAsync(x => x.Id == request.Id, cancellationToken) ?? throw new NotFoundException("Reward And Disciplinary Not Found");
        await rewardAndDisciplinaryRepository.UpdateAsync(mapper.Map(request.RewardAndDisciplinaryModel, rewardAndDisciplinary));
        await rewardAndDisciplinaryRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
        return Result.Success();
    }
}
