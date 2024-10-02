using EXE201_BE_ThrivoHR.Domain.Entities;

namespace EXE201_BE_ThrivoHR.Application.UseCase.V1.Salaries.Queries;

public record Filter(string EmployeeName, string EmployeeCode, int PageSize = 100, int PageNumber = 1) : ICommand<PagedResult<SalaryDto>>;
internal sealed class FilterHandler(ISalaryRepository salaryRepository, IMapper mapper) : ICommandHandler<Filter, PagedResult<SalaryDto>>
{
    public async Task<Result<PagedResult<SalaryDto>>> Handle(Filter request, CancellationToken cancellationToken)
    {


        IQueryable<Salary> query(IQueryable<Salary> x)
        {
            x = x.Where(x => (string.IsNullOrEmpty(request.EmployeeName) || x.Employee.FullName.Contains(request.EmployeeName))
            && (string.IsNullOrEmpty(request.EmployeeCode) || x.Employee.EmployeeCode.Contains(request.EmployeeCode))

            );
            return x;
        }
        var salaries = await salaryRepository.FindAllAsync(request.PageNumber, request.PageSize, query, cancellationToken);
        return Result.Success(PagedResult<SalaryDto>.Create(salaries.TotalCount, salaries.PageCount, salaries.PageSize, salaries.PageNo, salaries.ToSalariesDto(mapper)));
    }
}
