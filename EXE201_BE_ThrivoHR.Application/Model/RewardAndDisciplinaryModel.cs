using EXE201_BE_ThrivoHR.Domain.Common.Status;

namespace EXE201_BE_ThrivoHR.Application.Model;

public class RewardAndDisciplinaryModel
{
    public bool IsRewards { get; set; } = false;
    public string? EmployeeId { get; set; }
    public DateOnly Date { get; set; }
    public FormOfAction FormOfAction { get; set; }
    public decimal? Amount { get; set; }
    public string? Reason { get; set; }
    
    public FormStatus Status { get; set; } = FormStatus.Pending;
}
