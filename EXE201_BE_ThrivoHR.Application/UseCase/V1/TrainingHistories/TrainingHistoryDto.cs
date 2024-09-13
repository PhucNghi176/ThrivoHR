using EXE201_BE_ThrivoHR.Application.Common.Mappings;
using EXE201_BE_ThrivoHR.Application.Model;
using EXE201_BE_ThrivoHR.Application.UseCase.V1.Users;

namespace EXE201_BE_ThrivoHR.Application.UseCase.V1.TrainingHistories;

public class TrainingHistoryDto : IMapFrom<Domain.Entities.TrainingHistory>
{
    public int Id { get; set; }
    public DateOnly StartDay { get; set; }
    public string? WorkshopName { get; set; }
    public string? Content { get; set; }
    public string? Status { get; set; }
    public required EmployeeDto Employee { get; set; }
    public static TrainingHistoryDto Create(Domain.Entities.TrainingHistory trainingHistory)
    {
        return new TrainingHistoryDto
        {
            Id = trainingHistory.Id,
            StartDay = trainingHistory.StartDay,
            WorkshopName = trainingHistory.WorkshopName,
            Content = trainingHistory.Content,
            Status = trainingHistory.Status.ToString(),
            Employee = EmployeeDto.Create(trainingHistory.Employee!),
          
        };
    }


    public void Mapping(Profile profile)
    {
        profile.CreateMap<Domain.Entities.TrainingHistory, TrainingHistoryDto>()
            .ForMember(x => x.Employee, o => o.MapFrom(src => EmployeeDto.Create(src.Employee!)));

        profile.CreateMap<TrainingHistoryModelBase, Domain.Entities.TrainingHistory>();

    }
}
