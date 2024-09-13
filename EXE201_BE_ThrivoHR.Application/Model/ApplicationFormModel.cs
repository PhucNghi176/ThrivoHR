using EXE201_BE_ThrivoHR.Domain.Common.Status;

namespace EXE201_BE_ThrivoHR.Application.Model;

public class ApplicationFormModel
{
    public string? FullName { get; set; }
    public string? Email { get; set; }
    public string? PhoneNumber { get; set; }
    public DateOnly? DateOfBirth { get; set; } 
    public string? Address { get; set; }
    public string? City { get; set; }
    public string? Country { get; set; }
    public string? NationalID { get; set; }
    public bool? Gender { get; set; } 
    public string? EducationLevel { get; set; } 
    public string? EmploymentHistory { get; set; }
    public ApplicationStatus Status { get; set; } 
    public int? PositionId { get; set; } 
    public int? DepartmentId { get; set; } 
}
