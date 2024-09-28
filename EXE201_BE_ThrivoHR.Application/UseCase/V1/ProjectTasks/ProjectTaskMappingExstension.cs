using EXE201_BE_ThrivoHR.Domain.Entities;

namespace EXE201_BE_ThrivoHR.Application.UseCase.V1.ProjectTasks;

public static class ProjectTaskMappingExstension
{
    public static ProjectTaskDto ToProjectTaskDto(this ProjectTask projectTask, IMapper mapper)
        => mapper.Map<ProjectTaskDto>(projectTask);
    public static IEnumerable<ProjectTaskDto> ToProjectTaskDtos(this IEnumerable<ProjectTask> projectTasks, IMapper mapper)
        => mapper.Map<IEnumerable<ProjectTaskDto>>(projectTasks);

}
