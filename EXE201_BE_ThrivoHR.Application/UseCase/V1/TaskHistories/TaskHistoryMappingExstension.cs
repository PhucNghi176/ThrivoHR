using EXE201_BE_ThrivoHR.Domain.Entities;

namespace EXE201_BE_ThrivoHR.Application.UseCase.V1.TaskHistories;

public static class TaskHistoryMappingExstension
{
    public static TaskHistoryDto ToDto(this TaskHistory taskHistory, IMapper mapper)
    => mapper.Map<TaskHistoryDto>(taskHistory);
    public static IEnumerable<TaskHistoryDto> ToDtos(this IEnumerable<TaskHistory> taskHistories, IMapper mapper)
        => mapper.Map<IEnumerable<TaskHistoryDto>>(taskHistories);
}
