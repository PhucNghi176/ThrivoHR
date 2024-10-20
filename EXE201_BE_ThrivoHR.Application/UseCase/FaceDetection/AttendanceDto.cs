using EXE201_BE_ThrivoHR.Application.Common.Mappings;
using EXE201_BE_ThrivoHR.Domain.Entities;

namespace EXE201_BE_ThrivoHR.Application.UseCase.FaceDetection;
public class AttendanceDto : IMapFrom<Attendance>
{
    public string EmployeeName { get; set; }
    public DateOnly Date { get; set; }
    public TimeOnly? CheckIn { get; set; }
    public TimeOnly? CheckOut { get; set; }
    public string? Note { get; set; }

    
    public static AttendanceDto Create(Attendance attendance, IMapper mapper)
    {
        return mapper.Map<AttendanceDto>(attendance);
    }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<Attendance, AttendanceDto>()
            .ForMember(x => x.EmployeeName, o => o.MapFrom(src => src.Employee.FullName)).ReverseMap();
    }
}