using EXE201_BE_ThrivoHR.Application.Common.Exceptions;
using EXE201_BE_ThrivoHR.Application.Common.Method;
using EXE201_BE_ThrivoHR.Domain.Common.Status;

namespace EXE201_BE_ThrivoHR.Application.UseCase.V1.RewardAndDisciplinarys.Queries;

public record FilterRewardAndDisciplinary(
    string? EmployeeCode,
    FormOfAction? FormOfAction,
    FormStatus? Status,
   int PageSize = 100,
   int PageNumber = 1) : IQuery<PagedResult<RewardAndDisciplinaryDto>>;
internal sealed class FilterRewardAndDisciplinaryHandler(IRewardAndDisciplinaryRepository rewardAndDisciplinaryRepository, IMapper mapper) : IQueryHandler<FilterRewardAndDisciplinary, PagedResult<RewardAndDisciplinaryDto>>
{
    public async Task<Result<PagedResult<RewardAndDisciplinaryDto>>> Handle(FilterRewardAndDisciplinary request, CancellationToken cancellationToken)
    {
       
        var result = await rewardAndDisciplinaryRepository.FindAllAsync(
            
            request.PageNumber,
            request.PageSize,
            cancellationToken);
        return Result.Success(PagedResult<RewardAndDisciplinaryDto>.Create(result.TotalCount, result.PageCount, result.PageSize, result.PageNo, result.MapToListRewardAndDisciplinaryDto(mapper)));
    }
}

