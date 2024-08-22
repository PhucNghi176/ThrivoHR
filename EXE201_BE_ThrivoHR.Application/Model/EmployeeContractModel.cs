namespace EXE201_BE_ThrivoHR.Application.Model;

public class EmployeeContractModel
{
    public required string EmployeeCode { get; set; }
    public required int DepartmentId { get; set; }
    public required int PositionId { get; set; }
    public required DateOnly StartDate { get; set; }
    public DateOnly? EndDate { get; set; }
    public string? Notes { get; set; }

    public decimal Salary { get; set; }
}
