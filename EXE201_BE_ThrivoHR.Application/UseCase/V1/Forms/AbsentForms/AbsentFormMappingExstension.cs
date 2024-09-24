using EXE201_BE_ThrivoHR.Domain.Entities.Forms;

namespace EXE201_BE_ThrivoHR.Application.UseCase.V1.Forms.AbsentForms;

public static class AbsentFormMappingExstension
{
    public static AbsentFormDto MapToAbsentFormDto(this AbsentForm absentFormDto, IMapper mapper) =>
        mapper.Map<AbsentFormDto>(absentFormDto);
    public static List<AbsentFormDto> MapToAbsentFormListDto(this IEnumerable<AbsentForm> absentForms, IMapper mapper) =>
        absentForms.Select(absentForm => absentForm.MapToAbsentFormDto(mapper)).ToList();
}
