using EXE201_BE_ThrivoHR.Application.Common.Mappings;
using EXE201_BE_ThrivoHR.Domain.Entities;
using EXE201_BE_ThrivoHR.Domain.Entities.Identity;

namespace EXE201_BE_ThrivoHR.Application.UseCase.V1.TaskHistories;

public class TaskHistoryDto : IMapFrom<TaskHistory>
{
    public string Id { get; set; }

    public string? TaskId { get; set; }
    public string? ChangedByName { get; set; }
    public DateTime ChangeDate { get; set; }
    public string? OldStatus { get; set; }
    public string? NewStatus { get; set; }

    public string? OldAssigneeName { get; set; }
    public string? NewAssigneeName { get; set; }

    public DateTime? OldDueDate { get; set; }
    public DateTime? NewDueDate { get; set; }

    public double? OldProgress { get; set; }
    public double? NewProgress { get; set; }

    public string? ChangeDescription { get; set; }


    public static TaskHistoryDto Create(TaskHistory taskHistory ,IMapper mapper)
    {
        return mapper.Map<TaskHistoryDto>(taskHistory);
    }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<TaskHistory, TaskHistoryDto>()
            .ForMember(d => d.ChangedByName, opt => opt.MapFrom(s => s.ChangedBy!.FullName))
            .ForMember(d => d.OldStatus, opt => opt.MapFrom(s => s.OldStatus!.ToString()))
            .ForMember(d => d.NewStatus, opt => opt.MapFrom(s => s.NewStatus!.ToString()))
            .ForMember(d => d.OldAssigneeName, opt => opt.MapFrom(s => s.OldAssigneeId == null ? null : s.OldAssignee.FullName))
            .ForMember(d => d.NewAssigneeName, opt => opt.MapFrom(s => s.NewAssigneeId == null ? null : s.NewAssignee.FullName))
            .ReverseMap();



    }
}
