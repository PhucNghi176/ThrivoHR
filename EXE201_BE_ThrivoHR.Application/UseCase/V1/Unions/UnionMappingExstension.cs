using EXE201_BE_ThrivoHR.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EXE201_BE_ThrivoHR.Application.UseCase.V1.Unions;

public static class UnionMappingExstension
{
    public static UnionDto MapToUnionDto(this Union union, IMapper mapper) => mapper.Map<UnionDto>(union);
    public static List<UnionDto> MapToUnionListDto(this IEnumerable<Union> unions, IMapper mapper) => unions.Select(union => union.MapToUnionDto(mapper)).ToList();
}
