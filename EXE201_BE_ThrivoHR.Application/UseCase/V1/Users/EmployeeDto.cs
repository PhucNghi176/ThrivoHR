
using EXE201_BE_ThrivoHR.Application.Common.Mappings;
using EXE201_BE_ThrivoHR.Application.Model;
using EXE201_BE_ThrivoHR.Application.UseCase.V1.Addresses;
using EXE201_BE_ThrivoHR.Domain.Entities.Identity;

namespace EXE201_BE_ThrivoHR.Application.UseCase.V1.Users;

public class EmployeeDto : IMapFrom<AppUser>
{
    public string? EmployeeCode { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? FullName { get; set; }
    public string? Email { get; set; }
    public bool Sex { get; set; }
    public string? Religion { get; set; }
    public string? Ethnicity { get; set; }
    public string? IdentityNumber { get; set; }
    public string? PhoneNumber { get; set; }
    public string? TaxCode { get; set; }
    public string? BankAccount { get; set; }
    public AddressDto? Address { get; set; }
    public string? Department { get; set; }
    public string? Position { get; set; }
    public DateOnly? DateOfBirth { get; set; }
    public double NumberOfLeave { get; set; }
    public string? ImageUrl { get; set; }
    public string? Manager { get; set; }

    public static EmployeeDto Create(AppUser employee, IMapper mapper)
    {
        return mapper.Map<EmployeeDto>(employee);
    }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<AppUser, EmployeeDto>()
            .ForMember(x => x.Position, o => o.MapFrom(src => src.Position != null ? src.Position.Name : null))
            .ForMember(x => x.Department, o => o.MapFrom(src => src.Department != null ? src.Department.Name : null))
            .ForMember(x => x.Manager, o => o.MapFrom(src => src.Manager != null ? src.Manager.FullName : null)).ReverseMap();
        profile.CreateMap<EmployeeModel, AppUser>();


    }
}
