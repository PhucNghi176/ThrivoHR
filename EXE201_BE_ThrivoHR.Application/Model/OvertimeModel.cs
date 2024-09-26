using EXE201_BE_ThrivoHR.Domain.Common.Status;

namespace EXE201_BE_ThrivoHR.Application.Model;

public class OvertimeModel 
{
    public string? EmployeeId { get; set; }
    public DateOnly Date { get; set; }
    public int From { get; set; }
    public int To { get; set; }
    public string? Reason { get; set; }
    public FormStatus? Status { get; set; }
    public decimal? Amount { get; set; }
}
