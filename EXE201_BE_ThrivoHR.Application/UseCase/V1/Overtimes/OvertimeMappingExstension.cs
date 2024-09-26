using EXE201_BE_ThrivoHR.Domain.Entities;

namespace EXE201_BE_ThrivoHR.Application.UseCase.V1.Overtimes;

public static class OvertimeMappingExstension
{
    public static OvertimeDto MapToOvertimeDto(this Overtime overtime, IMapper mapper)
    => mapper.Map<OvertimeDto>(overtime);
    public static List<OvertimeDto> MapToListOvertimeDto(this IEnumerable<Overtime> overtimes, IMapper mapper)
        => overtimes.Select(x => x.MapToOvertimeDto(mapper)).ToList();
}
