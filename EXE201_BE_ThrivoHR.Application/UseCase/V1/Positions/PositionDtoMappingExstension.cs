using EXE201_BE_ThrivoHR.Domain.Entities;

namespace EXE201_BE_ThrivoHR.Application.UseCase.V1.Positions;

public static class PositionDtoMappingExstension
{
    public static PositionDto MapToPositionDto(this Position position, IMapper mapper)
    {
        return mapper.Map<PositionDto>(position);
    }
    public static List<PositionDto> MapToPositionListDto(this IEnumerable<Position> positions, IMapper mapper)
    {
        return positions.Select(position => position.MapToPositionDto(mapper)).ToList();
    }
}
