using EXE201_BE_ThrivoHR.Application.Common.Mappings;
using EXE201_BE_ThrivoHR.Application.Model;
using EXE201_BE_ThrivoHR.Domain.Common.Status;
using EXE201_BE_ThrivoHR.Domain.Entities;

namespace EXE201_BE_ThrivoHR.Application.UseCase.V1.Overtimes;

public class OvertimeDto : IMapFrom<Overtime>
{
    public new string Id { get; set; }
    public string? EmployeeId { get; set; }
    public string? EmployeeName { get; set; }
    public DateOnly Date { get; set; }
    public int From { get; set; }
    public int To { get; set; }
    public string? Reason { get; set; }
    public FormStatus Status { get; set; }
    public bool IsPaid { get; set; } = false;
    public decimal? Amount { get; set; }
    public string? ApproverName { get; set; }

    public static OvertimeDto Create(Overtime overtime, IMapper mapper)
    {
        return mapper.Map<OvertimeDto>(overtime);
    }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Overtime, OvertimeDto>()
            .ForMember(d => d.EmployeeName, opt => opt.MapFrom(s => s.Employee.FullName))
            .ForMember(d => d.ApproverName, opt => opt.MapFrom(s => s.Approver.FullName))
            .ForMember(d => d.Status, opt => opt.MapFrom(s => s.Status.ToString()))
            .ReverseMap();
        profile.CreateMap<OvertimeModel, Overtime>();

    }
}
