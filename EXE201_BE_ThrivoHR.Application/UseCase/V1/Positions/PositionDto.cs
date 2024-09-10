using EXE201_BE_ThrivoHR.Application.Common.Mappings;
using EXE201_BE_ThrivoHR.Domain.Entities;
using System.Runtime.InteropServices;

namespace EXE201_BE_ThrivoHR.Application.UseCase.V1.Positions;

public class PositionDto : IMapFrom<Position>
{
    public int Id { get; set; }
    public string? Name { get; set; }

    public static PositionDto Create(Position position)
    {
        return new PositionDto
        {
            Id = position.Id,
            Name = position.Name,
        };
    }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<Position, PositionDto>();
    }
}
