using EXE201_BE_ThrivoHR.Domain.Entities.Forms;

namespace EXE201_BE_ThrivoHR.Application.UseCase.V1.Forms.ResignForms;

public static class ResignFormMappingExstension
{

    public static ResginFormDto MapToResignFormDto(this ResignForm entity, IMapper mapper)
    {
        return mapper.Map<ResginFormDto>(entity);
    }

    public static List<ResginFormDto> MapToResignFormListDto(this IEnumerable<ResignForm> entity, IMapper mapper)
    {
        return entity.Select(x => x.MapToResignFormDto(mapper)).ToList();
    }
}
