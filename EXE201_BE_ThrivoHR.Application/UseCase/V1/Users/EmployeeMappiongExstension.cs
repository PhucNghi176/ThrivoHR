
using EXE201_BE_ThrivoHR.Domain.Entities.Identity;

namespace EXE201_BE_ThrivoHR.Application.UseCase.V1.Users;

public static class EmployeeMappiongExstension
{
    public static EmployeeDto MapTopEmployeeDto(this AppUser appUser, IMapper mapper)
    {
        return mapper.Map<EmployeeDto>(appUser);
    }

    public static List<EmployeeDto> MapToEmployeeListDto(this IEnumerable<AppUser> appUsers, IMapper mapper)
    {
        return appUsers.Select(appUser => appUser.MapTopEmployeeDto(mapper)).ToList();
    }
}
