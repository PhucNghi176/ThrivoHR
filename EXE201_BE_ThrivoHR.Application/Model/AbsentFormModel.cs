namespace EXE201_BE_ThrivoHR.Application.Model;

public class AbsentFormModel
{
    public string? EmployeeCode { get; set; }
    public DateTime From { get; set; }
    public DateTime To { get; set; }
    public string? Reason { get; set; }

}
