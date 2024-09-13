using EXE201_BE_ThrivoHR.Domain.Entities.Forms;

namespace EXE201_BE_ThrivoHR.Application.UseCase.V1.Forms.ApplicationForms;

public static class ApplicationFormMappingExstension
{
    public static ApplicationFormDto MapToApplicationFomrDto(this ApplicationForm entity, IMapper mapper)
    => mapper.Map<ApplicationFormDto>(entity);
    public static List<ApplicationFormDto> MapToApplicationFormListDto(this IEnumerable<ApplicationForm> applicationForms, IMapper mapper) => applicationForms.Select(applicationForm => applicationForm.MapToApplicationFomrDto(mapper)).ToList();
}
