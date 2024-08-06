namespace EXE201_BE_ThrivoHR.Application.Model;

public class EmployeeModel
{
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string FullName { get; set; }
    public required string IdentityNumber { get; set; }
    public required DateOnly DateOfBirth { get; set; }
    public required string PhoneNumber { get; set; }
    public string? TaxCode { get; set; }
    public required int DepartmentId { get; set; }
    public required int PositionId { get; set; }
    public required string BankAccount { get; set; }

    
    public required AddressModel Address { get; set; }

}
