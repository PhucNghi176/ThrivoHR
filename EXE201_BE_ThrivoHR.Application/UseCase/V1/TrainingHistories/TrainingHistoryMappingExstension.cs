using EXE201_BE_ThrivoHR.Domain.Entities;

namespace EXE201_BE_ThrivoHR.Application.UseCase.V1.TrainingHistories;

public static class TrainingHistoryMappingExstension
{
    public static TrainingHistoryDto MapToTrainingHistoryDto(this TrainingHistory trainingHistory, IMapper mapper)
    {
        return mapper.Map<TrainingHistoryDto>(trainingHistory);
    }
    public static List<TrainingHistoryDto> MapToTrainingHistoryListDto(this IEnumerable<TrainingHistory> trainingHistories, IMapper mapper)
    {
        return trainingHistories.Select(trainingHistory => trainingHistory.MapToTrainingHistoryDto(mapper)).ToList();
    }
}
