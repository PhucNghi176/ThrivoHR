using EXE201_BE_ThrivoHR.Application.Common.Mappings;
using EXE201_BE_ThrivoHR.Application.Model;
using EXE201_BE_ThrivoHR.Domain.Entities.Forms;

namespace EXE201_BE_ThrivoHR.Application.UseCase.V1.Forms.AbsentForms;

public class AbsentFormDto : IMapFrom<AbsentForm>
{
    public string Id { get; set; }
    public DateTime? CreatedDay { get; set; }
    public string? EmployeeCode { get; set; }
    public DateTime From { get; set; }
    public DateTime To { get; set; }
    public string? Reason { get; set; }
    public string? ApproverName { get; set; }
    public string? Status { get; set; }

    public static AbsentFormDto Create(AbsentForm entity)
    {
        return new AbsentFormDto
        {
            Id = entity.Id,
            CreatedDay = entity.CreatedOn,
            EmployeeCode = entity.Employee?.EmployeeCode,
            From = entity.From,
            To = entity.To,
            Reason = entity.Reason,
            ApproverName = entity.Approver?.FullName,
            Status = entity.Status.ToString(),
        };
    }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<AbsentForm, AbsentFormDto>()
            .ForMember(dest => dest.EmployeeCode, opt => opt.MapFrom(src => src.Employee.EmployeeCode))
            .ForMember(dest => dest.ApproverName, opt => opt.MapFrom(src => src.Approver.FullName))
            .ForMember(dest => dest.CreatedDay, opt => opt.MapFrom(src => src.CreatedOn));
        profile.CreateMap<AbsentForm, AbsentFormModel>().ReverseMap();
    }
}
