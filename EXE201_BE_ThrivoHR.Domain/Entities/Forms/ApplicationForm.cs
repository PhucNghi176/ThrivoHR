using EXE201_BE_ThrivoHR.Domain.Common.Status;
using EXE201_BE_ThrivoHR.Domain.Entities.Base;

namespace EXE201_BE_ThrivoHR.Domain.Entities.Forms;

public class ApplicationForm : BaseForm
{
    public string? FullName { get; set; }
    public string? Email { get; set; }
    public string? PhoneNumber { get; set; }
    public DateOnly? DateOfBirth { get; set; } // DateOnly is available in newer C# versions for date without time.
    public string? Address { get; set; }
    public string? City { get; set; }
    public string? Country { get; set; }
    public string? NationalID { get; set; } // For identification number (e.g., passport, social security, etc.)
    public bool? Gender { get; set; } // Optional gender field
    public string? EducationLevel { get; set; } // E.g., High School, Bachelor's, Master's, etc.
    public string? EmploymentHistory { get; set; } // Could be a more detailed model or summary of employment
    public int? PositionId { get; set; } // Foreign key to Position
    public virtual Position? Position { get; set; } // Navigation property to Position

    public int? DepartmentId { get; set; } // Foreign key to Department
    public virtual Department? Department { get; set; } // Navigation property to Department

}

