namespace EXE201_BE_ThrivoHR.Application.Model;

public class ResginFormModel
{
    public string? EmployeeCode { get; set; }
    public DateTime DateTime { get; set; }
    public string? Reason { get; set; }
    public DateOnly LastWorkingDate { get; set; }
}
