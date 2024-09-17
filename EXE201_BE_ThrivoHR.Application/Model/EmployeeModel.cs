namespace EXE201_BE_ThrivoHR.Application.Model;

public record EmployeeModel
{

    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string FullName { get; set; }
    public required string IdentityNumber { get; set; }
    public required bool Sex { get; set; }
    public required string Religion { get; set; }
    public required string Ethnicity { get; set; }
    public required DateOnly DateOfBirth { get; set; }
    public required string PhoneNumber { get; set; }
    public string? TaxCode { get; set; }
    public required string BankAccount { get; set; }
    public required string Email { get; set; }

    public required AddressModel Address { get; set; }
}
public record EmployeeModelRequest : EmployeeModel
{
    public string? EmployeeCode { get; set; }
}


