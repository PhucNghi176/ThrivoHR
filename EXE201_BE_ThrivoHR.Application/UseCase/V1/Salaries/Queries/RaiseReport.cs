
namespace EXE201_BE_ThrivoHR.Application.UseCase.V1.Salaries.Queries;

public record RaiseReport(string? EmployeeCode, string? EmployeeName, int PageSize = 100, int PageNumber = 1) : ICommand<List<RaiseDto>>;
internal sealed class RaiseReportHandler : ICommandHandler<RaiseReport, List<RaiseDto>>
{
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IEmployeeContractRepository _employeeContractRepository;

    public RaiseReportHandler(IEmployeeRepository employeeRepository, IEmployeeContractRepository employeeContractRepository)
    {
        _employeeRepository = employeeRepository;
        _employeeContractRepository = employeeContractRepository;
    }

    public async Task<Result<List<RaiseDto>>> Handle(RaiseReport request, CancellationToken cancellationToken)
    {
        var employees = await _employeeRepository.FindAllAsync(request.PageNumber, request.PageSize, cancellationToken);

        List<RaiseDto> result = new List<RaiseDto>();
        foreach (var item in employees)
        {
            var employee = item;
            var contract = await _employeeContractRepository.FindAsync(x => x.EmployeeId == employee.Id, cancellationToken);
            if (contract == null)
            {
                continue;
            }
            var raise = new RaiseDto
            {
                EmployeeCode = employee.EmployeeCode,
                EmployeeFullName = employee.FullName,
                Date = DateOnly.FromDateTime(DateTime.Now),
                OldSalary = contract.Salary,
                NewSalary = contract.Salary * 1.1M,
                Reason = "Raise 10%"
            };
            result.Add(raise);
        }




        return Result.Success(result);
    }
}


class RaiseDto
{
    public string? EmployeeCode { get; set; }
    public string? EmployeeFullName { get; set; }
    public DateOnly Date { get; set; }
    public decimal? OldSalary { get; set; }
    public decimal? NewSalary { get; set; }
    public string? Reason { get; set; }
}





