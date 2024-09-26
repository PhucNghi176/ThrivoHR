using EXE201_BE_ThrivoHR.Domain.Common.Status;
using EXE201_BE_ThrivoHR.Domain.Entities;

namespace EXE201_BE_ThrivoHR.Application.UseCase.V1.Overtimes.Queries;

public record Fitler(

    string? EmployeeId,
    DateOnly? Date,
    int? From,
    int? To,
    string? Reason,
    FormStatus? Status,
    bool? IsPaid,
    decimal? Amount,
    int PageNumber = 1,
    int PageSize = 100) : IQuery<PagedResult<OvertimeDto>>;

internal sealed class FitlerHandler(IOvertimeRepository overtimeRepository, IMapper mapper) : IQueryHandler<Fitler, PagedResult<OvertimeDto>>
{
    public async Task<Result<PagedResult<OvertimeDto>>> Handle(Fitler request, CancellationToken cancellationToken)
    {
       
        IQueryable<Overtime> filter(IQueryable<Overtime> x)
        {
            x=x.Where(x =>
                (string.IsNullOrEmpty(request.EmployeeId) || x.EmployeeId == request.EmployeeId)
                && (!request.Date.HasValue || x.Date == request.Date)
                && (!request.From.HasValue || x.From == request.From)
                && (!request.To.HasValue || x.To == request.To)
                && (string.IsNullOrEmpty(request.Reason) || x.Reason.Contains(request.Reason))
                && (!request.Status.HasValue || x.Status == request.Status)
                && (!request.IsPaid.HasValue || x.IsPaid == request.IsPaid)
                && (!request.Amount.HasValue || x.Amount == request.Amount));

            return x;
            
        }
        var list = await overtimeRepository.FindAllAsync(request.PageNumber, request.PageSize, filter,cancellationToken);
        var page = PagedResult<OvertimeDto>.Create(list.TotalCount, list.PageCount, list.PageSize, list.PageNo, list.MapToListOvertimeDto(mapper));
        return Result.Success(page);

        
    }
}

