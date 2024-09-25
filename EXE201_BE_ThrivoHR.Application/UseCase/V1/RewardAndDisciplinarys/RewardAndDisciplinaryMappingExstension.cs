using EXE201_BE_ThrivoHR.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EXE201_BE_ThrivoHR.Application.UseCase.V1.RewardAndDisciplinarys;

public static class RewardAndDisciplinaryMappingExstension
{
    public static RewardAndDisciplinaryDto MapToRewardAndDisciplinaryDto(this RewardsAndDisciplinary rewardAndDisciplinary, IMapper mapper) => mapper.Map<RewardAndDisciplinaryDto>(rewardAndDisciplinary);
    public static List<RewardAndDisciplinaryDto> MapToListRewardAndDisciplinaryDto(this IEnumerable<RewardsAndDisciplinary> rewardAndDisciplinaries, IMapper mapper) => rewardAndDisciplinaries.Select(x => x.MapToRewardAndDisciplinaryDto(mapper)).ToList();

}
