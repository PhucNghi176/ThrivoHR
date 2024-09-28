using EXE201_BE_ThrivoHR.Application.Common.Mappings;
using EXE201_BE_ThrivoHR.Application.Model;
using EXE201_BE_ThrivoHR.Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace EXE201_BE_ThrivoHR.Application.UseCase.V1.RewardAndDisciplinarys;

public class RewardAndDisciplinaryDto : IMapFrom<RewardsAndDisciplinary>
{
    public string Id { get; set; }
    public bool IsRewards { get; set; }
    public string? EmployeeId { get; set; }
    public string? EmployeeName { get; set; }
    public DateOnly Date { get; set; }
    public string FormOfAction { get; set; }
    [Column(TypeName = "decimal(18, 2)")]
    public decimal? Amount { get; set; }
    public string? Reason { get; set; }
    public string? ApproverName { get; set; }
    public string Status { get; set; }

    public static RewardAndDisciplinaryDto Create(RewardAndDisciplinaryDto rewardAndDisciplinaryDto, IMapper mapper)
    {
        return mapper.Map<RewardAndDisciplinaryDto>(rewardAndDisciplinaryDto);
    }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<RewardsAndDisciplinary, RewardAndDisciplinaryDto>()
            .ForMember(d => d.EmployeeName, opt => opt.MapFrom(s => s.Employee.FullName))
            .ForMember(d => d.ApproverName, opt => opt.MapFrom(s => s.Approver.FullName))
            .ForMember(d => d.Status, opt => opt.MapFrom(s => s.Status.ToString()))
            .ForMember(d => d.FormOfAction, opt => opt.MapFrom(s => s.FormOfAction.ToString()))


            .ReverseMap();

        profile.CreateMap<RewardAndDisciplinaryModel, RewardsAndDisciplinary>();




    }
}
