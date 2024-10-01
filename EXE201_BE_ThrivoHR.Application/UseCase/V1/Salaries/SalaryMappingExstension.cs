using EXE201_BE_ThrivoHR.Domain.Entities;

namespace EXE201_BE_ThrivoHR.Application.UseCase.V1.Salaries;

public static class SalaryMappingExstension
{
    public static SalaryDto ToSalaryDto(this Salary salary, IMapper mapper)
       => mapper.Map<SalaryDto>(salary);
    public static IEnumerable<SalaryDto> ToSalariesDto(this IEnumerable<Salary> salaries, IMapper mapper)
        => mapper.Map<IEnumerable<SalaryDto>>(salaries);
}
