using EXE201_BE_ThrivoHR.Domain.Entities;

namespace EXE201_BE_ThrivoHR.Application.UseCase.V1.Projects;

public static class ProjectDtoMappingExstension
{
    public static ProjectDto ToProjectDto(this Project project, IMapper mapper)
    => mapper.Map<ProjectDto>(project);
    public static IEnumerable<ProjectDto> ToListProjectDtos(this IEnumerable<Project> projects, IMapper mapper) =>
        mapper.Map<IEnumerable<ProjectDto>>(projects);
}
