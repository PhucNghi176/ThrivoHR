using EXE201_BE_ThrivoHR.Application.Common.Method;
using EXE201_BE_ThrivoHR.Domain.Entities.Contracts;

namespace EXE201_BE_ThrivoHR.Application.UseCase.V1.Contracts.EmployeeContracts.Queries;

public record FilterEmployeeContract(
    string? EmployeeCode,
    DateOnly? StartDate,
    DateOnly? EndDate,
    string? Notes,
    decimal? Salary,
     int? Department = 0,
    int? Position = 0,
    int PageNumber = 1,
    int PageSize = 100

    ) : IQuery<PagedResult<EmployeeContractDto>>;

internal sealed class FilterEmployeeContractHandler : IQueryHandler<FilterEmployeeContract, PagedResult<EmployeeContractDto>>
{
    private readonly IEmployeeContractRepository _employeeContractRepository;
    private readonly IMapper _mapper;

    public FilterEmployeeContractHandler(IEmployeeContractRepository employeeContractRepository, IMapper mapper)
    {
        _employeeContractRepository = employeeContractRepository;
        _mapper = mapper;
    }

    public async Task<Result<PagedResult<EmployeeContractDto>>> Handle(FilterEmployeeContract request, CancellationToken cancellationToken)
    {

        IQueryable<EmployeeContract> filter(IQueryable<EmployeeContract> x)
        {
            x = x.OfType<EmployeeContract>()
                .Where(x =>
                (string.IsNullOrEmpty(request.EmployeeCode) || x.Employee!.EmployeeId == EmployeesMethod.ConvertEmployeeCodeToId(request.EmployeeCode))
                && (!request.StartDate.HasValue || x.StartDate == request.StartDate)
                && (!request.EndDate.HasValue || x.EndDate == request.EndDate)
                && (string.IsNullOrEmpty(request.Notes) || x.Notes!.Contains(request.Notes))
                && (!request.Salary.HasValue || x.Salary == request.Salary)
                && (request.Department == 0 || x.DepartmentId == request.Department)
                && (request.Position == 0 || x.PositionId == request.Position));

            return x;
        }
        var employeeContracts = await _employeeContractRepository.FindAllAsync(request.PageNumber, request.PageSize, filter, cancellationToken);
        var res = PagedResult<EmployeeContractDto>.Create(employeeContracts.TotalCount, employeeContracts.PageCount, employeeContracts.PageSize, employeeContracts.PageNo, employeeContracts.MapToEmployeeContractListDto(_mapper));
        return Result.Success(res);
    }
}