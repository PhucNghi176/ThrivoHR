namespace EXE201_BE_ThrivoHR.Application.UseCase.V1.Contracts.EmployeeContracts.Commands;

public record GenerateEmployeeContract : ICommand;
internal sealed class GenerateEmployeeContractHandler : ICommandHandler<GenerateEmployeeContract>
{
    private readonly IEmployeeContractRepository _employeeContractRepository;
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IMapper _mapper;

    public GenerateEmployeeContractHandler(IEmployeeContractRepository employeeContractRepository, IMapper mapper, IEmployeeRepository employeeRepository)
    {
        _employeeContractRepository = employeeContractRepository;
        _mapper = mapper;
        _employeeRepository = employeeRepository;
    }

    public async Task<Result> Handle(GenerateEmployeeContract request, CancellationToken cancellationToken)
    {
        var list = await _employeeRepository.FindAllAsync(cancellationToken);
        var employees = list.Select(x => new { x.Id, x.DepartmentId, x.PositionId }).ToList();
        foreach (var employee in employees)
        {
            Random random = new Random();
            // Generate random salary
            var salary = random.Next(10000000, 1000000000);
            var startDate = DateOnly.FromDateTime(DateTime.Now);
            var IsNoExpiry = random.Next(0, 2) == 1;
            DateOnly? endDate = !IsNoExpiry ? DateOnly.FromDateTime(DateTime.Now.AddYears(random.Next(1, 4))) : null;


            var duration = IsNoExpiry ? 0 : (endDate!.Value.Year - startDate.Year) * 12;


            var employeeContract = new Domain.Entities.Contracts.EmployeeContract
            {
                EmployeeId = employee.Id,
                DepartmentId = employee.DepartmentId,
                PositionId = employee.PositionId,
                StartDate = startDate,
                EndDate = !IsNoExpiry? endDate:null,
                Salary = salary,
                IsNoExpiry = IsNoExpiry,
                Duration = duration
            };
            await _employeeContractRepository.AddAsync(employeeContract);
        }
        await _employeeContractRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
        return Result.Success();


    }
}

