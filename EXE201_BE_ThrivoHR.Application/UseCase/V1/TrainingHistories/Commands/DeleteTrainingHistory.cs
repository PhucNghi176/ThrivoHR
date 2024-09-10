
using EXE201_BE_ThrivoHR.Domain.Common.Exceptions;

namespace EXE201_BE_ThrivoHR.Application.UseCase.V1.TrainingHistories.Commands;

public record DeleteTrainingHistory(int ID) : ICommand;
internal sealed class DeleteTrainingHistoryHandler : ICommandHandler<DeleteTrainingHistory>
{
    private readonly ITrainingHistoryRepository _trainingHistoryRepository;

    public DeleteTrainingHistoryHandler(ITrainingHistoryRepository trainingHistoryRepository)
    {
        _trainingHistoryRepository = trainingHistoryRepository;
    }

    public async Task<Result> Handle(DeleteTrainingHistory request, CancellationToken cancellationToken)
    {
        var trainingHistory = await _trainingHistoryRepository.FindAsync(x => x.Id == request.ID, cancellationToken) ?? throw new NotFoundException("Training History Not Found");
        trainingHistory.IsDeleted = true;
        await _trainingHistoryRepository.UpdateAsync(trainingHistory);
        await _trainingHistoryRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
        return Result.Success();


    }
}

