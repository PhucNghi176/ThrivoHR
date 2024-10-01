namespace EXE201_BE_ThrivoHR.Application.UseCase.V1.Salaries.Queries;

public record Filter(int PageSize = 100, int PageNumber = 1) : ICommand<PagedResult<SalaryDto>>;
internal sealed class FilterHandler(ISalaryRepository salaryRepository, IMapper mapper) : ICommandHandler<Filter, PagedResult<SalaryDto>>
{
    public async Task<Result<PagedResult<SalaryDto>>> Handle(Filter request, CancellationToken cancellationToken)
    {
        var salaries = await salaryRepository.FindAllAsync(request.PageNumber, request.PageSize, cancellationToken);
        return Result.Success(PagedResult<SalaryDto>.Create(salaries.TotalCount, salaries.PageCount, salaries.PageSize, salaries.PageNo, salaries.ToSalariesDto(mapper)));
    }
}
