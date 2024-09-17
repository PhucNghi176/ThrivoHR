using EXE201_BE_ThrivoHR.Application.Common.Mappings;
using EXE201_BE_ThrivoHR.Application.Model;
using EXE201_BE_ThrivoHR.Domain.Entities;

namespace EXE201_BE_ThrivoHR.Application.UseCase.V1.Unions;

public class UnionDto : IMapFrom<Union>
{
    public int Id { get; set; }
    public string? EmployeeCode { get; set; }
    public string? Title { get; set; }
    public DateOnly DateJoined { get; set; }
    public static UnionDto Create(Union union)
    {
        return new UnionDto
        {
            Id = union.Id,
            EmployeeCode =union.Employee!.EmployeeCode,
            Title = union.Title,
            DateJoined = union.DateJoined
        };
    }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<Union, UnionDto>()
            .ForMember(d => d.EmployeeCode, opt => opt.MapFrom(s => s.Employee!.EmployeeCode));
        profile.CreateMap<Union, UnionModel>().ReverseMap();
    }
}
