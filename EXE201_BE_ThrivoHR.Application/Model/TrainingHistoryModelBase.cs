using EXE201_BE_ThrivoHR.Domain.Common.Status;

namespace EXE201_BE_ThrivoHR.Application.Model;

public class TrainingHistoryModelBase
{
    public DateOnly StartDay { get; set; }
    public string? WorkshopName { get; set; }
    public string? Content { get; set; }
    public TrainingHistoryEnum Status { get; set; }
}
public class TrainingHistoryModelCreate : TrainingHistoryModelBase
{
    public required string EmployeeCode { get; set; }
}
public class TrainingHistoryModelUpdate : TrainingHistoryModelBase
{
    public int Id { get; set; }
}
