using EXE201_BE_ThrivoHR.Application.Common.Exceptions;
using EXE201_BE_ThrivoHR.Application.Common.Method;
using EXE201_BE_ThrivoHR.Domain.Common.Status;
using EXE201_BE_ThrivoHR.Domain.Entities;

namespace EXE201_BE_ThrivoHR.Application.UseCase.V1.RewardAndDisciplinarys.Queries;

public record FilterRewardAndDisciplinary(
    string? EmployeeCode,
    bool? IsRewards,
    FormOfAction? FormOfAction,
    FormStatus? Status,
   int PageSize = 100,
   int PageNumber = 1) : IQuery<PagedResult<RewardAndDisciplinaryDto>>;
internal sealed class FilterRewardAndDisciplinaryHandler(IRewardAndDisciplinaryRepository rewardAndDisciplinaryRepository, IMapper mapper) : IQueryHandler<FilterRewardAndDisciplinary, PagedResult<RewardAndDisciplinaryDto>>
{
    public async Task<Result<PagedResult<RewardAndDisciplinaryDto>>> Handle(FilterRewardAndDisciplinary request, CancellationToken cancellationToken)
    {
        IQueryable<RewardsAndDisciplinary> filter(IQueryable<RewardsAndDisciplinary> x)
        {
            x = x.Where(x =>
                 (string.IsNullOrEmpty(request.EmployeeCode) || x.EmployeeId == request.EmployeeCode ||
                 (request.IsRewards == null || x.IsRewards == request.IsRewards) &&
                 (request.FormOfAction == null || x.FormOfAction == request.FormOfAction) &&
                 (request.Status == null || x.Status == request.Status)));
            return x;
        }

        var result = await rewardAndDisciplinaryRepository.FindAllAsync(

            request.PageNumber,
            request.PageSize,
            filter,
            cancellationToken);
        return Result.Success(PagedResult<RewardAndDisciplinaryDto>.Create(result.TotalCount, result.PageCount, result.PageSize, result.PageNo, result.MapToListRewardAndDisciplinaryDto(mapper)));
    }
}

