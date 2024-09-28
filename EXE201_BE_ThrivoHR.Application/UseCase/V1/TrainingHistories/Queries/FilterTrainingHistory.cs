namespace EXE201_BE_ThrivoHR.Application.UseCase.V1.TrainingHistories.Queries;

public record FilterTrainingHistory(
    string? EmployeeCode,
    DateOnly? StartDay,
    string? WorkshopName,
    string? Content,
    Domain.Common.Status.TrainingHistory? Status,
    int PageSize = 100,
    int PageNumber = 1
    ) : IQuery<PagedResult<TrainingHistoryDto>>;

internal sealed class FilterTrainingHistoryQueryHandler : IQueryHandler<FilterTrainingHistory, PagedResult<TrainingHistoryDto>>
{
    private readonly IMapper _mapper;
    private readonly ITrainingHistoryRepository _trainingHistoryRepository;

    public FilterTrainingHistoryQueryHandler(IMapper mapper, ITrainingHistoryRepository trainingHistoryRepository)
    {
        _mapper = mapper;
        _trainingHistoryRepository = trainingHistoryRepository;
    }

    public async Task<Result<PagedResult<TrainingHistoryDto>>> Handle(FilterTrainingHistory request, CancellationToken cancellationToken)
    {
        IQueryable<Domain.Entities.TrainingHistory> filter(IQueryable<Domain.Entities.TrainingHistory> x)
        {

            x = x.Where(x =>
                 (string.IsNullOrEmpty(request.EmployeeCode) || x.Employee!.EmployeeCode == request.EmployeeCode)
                 && (!request.StartDay.HasValue || x.StartDay == request.StartDay)
                 && (string.IsNullOrEmpty(request.WorkshopName) || x.WorkshopName.Contains(request.WorkshopName))
                 && (string.IsNullOrEmpty(request.Content) || x.Content.Contains(request.Content))
                 && (!request.Status.HasValue || x.Status == request.Status)
             );

            return x;
        }
        var list = await _trainingHistoryRepository.FindAllAsync(request.PageNumber, request.PageSize, filter, cancellationToken);
        var mapped = PagedResult<TrainingHistoryDto>.Create(list.TotalCount, list.PageCount, list.PageSize, list.PageNo, list.MapToTrainingHistoryListDto(_mapper));
        return Result.Success(mapped);

    }
}

