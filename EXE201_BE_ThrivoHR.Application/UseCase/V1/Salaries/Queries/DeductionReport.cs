﻿namespace EXE201_BE_ThrivoHR.Application.UseCase.V1.Salaries.Queries;

public record DeductionReport(string? EmployeeCode, string? EmployeeName, int PageSize = 100, int PageNumber = 1) : ICommand<List<DeductionDto>>;
internal sealed class DeductionReportHandler : ICommandHandler<DeductionReport, List<DeductionDto>>
{
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IEmployeeContractRepository _employeeContractRepository;

    public DeductionReportHandler(IEmployeeRepository employeeRepository, IEmployeeContractRepository employeeContractRepository)
    {
        _employeeRepository = employeeRepository;
        _employeeContractRepository = employeeContractRepository;
    }

    public async Task<Result<List<DeductionDto>>> Handle(DeductionReport request, CancellationToken cancellationToken)
    {
        var employees = await _employeeRepository.FindAllAsync(request.PageNumber, request.PageSize, cancellationToken);

        List<DeductionDto> result = new List<DeductionDto>();
        foreach (var item in employees)
        {
            var employee = item;
            var contract = await _employeeContractRepository.FindAsync(x => x.EmployeeId == employee.Id, cancellationToken);
            if (contract == null)
            {
                continue;
            }
            var deduction = new DeductionDto
            {
                EmployeeCode = employee.EmployeeCode,
                EmployeeFullName = employee.FullName,
                Date = DateOnly.FromDateTime(DateTime.Now),
                OldSalary = contract.Salary,
                NewSalary = contract.Salary * 0.9M,
                Reason = "Deduction 10%"
            };
            result.Add(deduction);
        }

        return Result.Success(result);
    }

   
}
class DeductionDto
{
    public string? EmployeeCode { get; set; }
    public string? EmployeeFullName { get; set; }
    public DateOnly Date { get; set; }
    public decimal? OldSalary { get; set; }
    public decimal? NewSalary { get; set; }
    public string? Reason { get; set; }
}