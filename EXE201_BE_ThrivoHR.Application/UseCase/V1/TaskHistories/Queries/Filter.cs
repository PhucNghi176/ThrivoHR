
using EXE201_BE_ThrivoHR.Domain.Entities;

namespace EXE201_BE_ThrivoHR.Application.UseCase.V1.TaskHistories.Queries;

public record Filter(string TaskId, int PageSize = 100, int PageNumber = 1) : IQuery<PagedResult<TaskHistoryDto>>;
internal sealed class FilterHandler(IMapper mapper, ITaskHistoryRepository taskHistoryRepository) : IQueryHandler<Filter, PagedResult<TaskHistoryDto>>
{
    public async Task<Result<PagedResult<TaskHistoryDto>>> Handle(Filter request, CancellationToken cancellationToken)
    {
        IQueryable<TaskHistory> filter(IQueryable<TaskHistory> x)
        {
            x = x.Where(x => x.TaskId == request.TaskId);


            return x;
        }

        var r = await taskHistoryRepository.FindAllAsync(request.PageNumber, request.PageSize, filter, cancellationToken);
        return Result.Success(PagedResult<TaskHistoryDto>.Create(r.TotalCount, r.PageCount, r.PageSize, r.PageNo, r.ToDtos(mapper)));
    }
}
