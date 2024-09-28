using EXE201_BE_ThrivoHR.Application.Common.Mappings;
using EXE201_BE_ThrivoHR.Application.Model;
using EXE201_BE_ThrivoHR.Domain.Entities;

namespace EXE201_BE_ThrivoHR.Application.UseCase.V1.ProjectTasks;

public class ProjectTaskDto : IMapFrom<ProjectTask>
{
    public string? Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public string? AssigneeName { get; set; }
    public DateTime? DueDate { get; set; }
    public double Progress { get; set; }
    public string? Status { get; set; }

    public static ProjectTaskDto Create(ProjectTask projectTask, IMapper mapper)
    {
        return mapper.Map<ProjectTaskDto>(projectTask);
    }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<ProjectTask, ProjectTaskDto>()
            .ForMember(d => d.AssigneeName, opt => opt.MapFrom(s => s.Assignee.FullName))
            .ForMember(d => d.Status, opt => opt.MapFrom(s => s.Status.ToString())).ReverseMap();
        profile.CreateMap<ProjectTask, ProjectTaskModel>().ReverseMap();
    }
}
