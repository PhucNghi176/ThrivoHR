namespace EXE201_BE_ThrivoHR.Application.Model;
public class EmployeeContractBase
{
    public required int DepartmentId { get; set; }
    public required int PositionId { get; set; }
    public required DateOnly StartDate { get; set; }
    public DateOnly? EndDate { get; set; }
    public string? Notes { get; set; }
    public decimal Salary { get; set; }
}
public class EmployeeContractModelCreate : EmployeeContractBase
{

    public required string EmployeeCode { get; set; }

}

public class EmployeeContractModelUpdate : EmployeeContractBase
{
    public required string ContractId { get; set; }
}
