using EXE201_BE_ThrivoHR.Application.Common.Mappings;
using EXE201_BE_ThrivoHR.Application.Model;
using EXE201_BE_ThrivoHR.Domain.Common.Status;
using EXE201_BE_ThrivoHR.Domain.Entities;
using EXE201_BE_ThrivoHR.Domain.Entities.Forms;

namespace EXE201_BE_ThrivoHR.Application.UseCase.V1.Forms.ApplicationForms;

public class ApplicationFormDto : IMapFrom<ApplicationForm>
{
    public string Id { get; set; }
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
    public string? Status { get; set; }
    public int? PositionId { get; set; }
    public int? DepartmentId { get; set; }

    public string? ApproverName { get; set; }
    public ApplicationFormDto Create(ApplicationForm entity)
    {
        return new ApplicationFormDto
        {
            Id = entity.Id,
            FullName = entity.FullName,
            Email = entity.Email,
            PhoneNumber = entity.PhoneNumber,
            DateOfBirth = entity.DateOfBirth,
            Address = entity.Address,
            City = entity.City,
            Country = entity.Country,
            NationalID = entity.NationalID,
            Gender = entity.Gender,
            EducationLevel = entity.EducationLevel,
            EmploymentHistory = entity.EmploymentHistory,
            Status = entity.Status.ToString(),
            PositionId = entity.PositionId,
            DepartmentId = entity.DepartmentId,
            ApproverName = entity.Approver?.FullName,


        };
    }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<ApplicationForm, ApplicationFormDto>().ReverseMap();
        profile.CreateMap<ApplicationForm, ApplicationFormModel>().ReverseMap();
    }
}
