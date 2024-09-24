
using EXE201_BE_ThrivoHR.Domain.Entities.Forms;

namespace EXE201_BE_ThrivoHR.Application.UseCase.V1.Forms.AbsentForms.Queries;

public record FilterAbsentForm(string? EmployeeCode, int PageSize = 100, int PageNumber = 1) : IQuery<PagedResult<AbsentFormDto>>;
internal sealed class FilterAbsentFormHandler(IAbsentFormRepository _absentFormRepository, IMapper _mapper) : IQueryHandler<FilterAbsentForm, PagedResult<AbsentFormDto>>
{


    public async Task<Result<PagedResult<AbsentFormDto>>> Handle(FilterAbsentForm request, CancellationToken cancellationToken)
    {
        IQueryable<AbsentForm> filter(IQueryable<AbsentForm> x)
        {
            //x = x.Where(x => string.IsNullOrEmpty(request.EmployeeCode) || x.EmployeeId.Contains(request.EmployeeCode));
            return x;
        }
        var list = await _absentFormRepository.FindAllAsync(request.PageNumber,request.PageSize,filter,cancellationToken);
        return Result.Success(PagedResult<AbsentFormDto>.Create(list.TotalCount, list.PageCount, list.PageSize, list.PageNo,list.MapToAbsentFormListDto(_mapper)));
    }
}
