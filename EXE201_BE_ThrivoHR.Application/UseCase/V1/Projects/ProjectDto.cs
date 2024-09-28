using EXE201_BE_ThrivoHR.Application.Common.Mappings;
using EXE201_BE_ThrivoHR.Application.Model;
using EXE201_BE_ThrivoHR.Domain.Entities;
namespace EXE201_BE_ThrivoHR.Application.UseCase.V1.Projects;
public class ProjectDto : IMapFrom<Project>
{
    public new string Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public string? LeaderName { get; set; }
    public int TotalEmployee { get; set; }
    public int TotalTask { get; set; }
    public string? SubLeaderName { get; set; }
    public double Progress { get; set; }
    public string? Status { get; set; }

    public static ProjectDto Create(Project project, IMapper mapper)
    {
        return mapper.Map<ProjectDto>(project);
    }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<Project, ProjectDto>()
            .ForMember(d => d.LeaderName, opt => opt.MapFrom(s => s.Leader.FullName))
            .ForMember(d => d.SubLeaderName, opt => opt.MapFrom(s => s.SubLeader.FullName))
            .ForMember(d => d.TotalEmployee, opt => opt.MapFrom(s => s.ProjectEmployees.Where(x => !x.IsDeleted).Count()))
            .ForMember(d => d.TotalTask, opt => opt.MapFrom(s => s.Tasks.Count))
            .ForMember(d => d.Status, opt => opt.MapFrom(s => s.Status.ToString())).ReverseMap();
        profile.CreateMap<ProjectModel, Project>().ReverseMap();
    }
}
