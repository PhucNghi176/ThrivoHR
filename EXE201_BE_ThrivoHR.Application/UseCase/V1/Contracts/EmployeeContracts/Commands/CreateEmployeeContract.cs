
using EXE201_BE_ThrivoHR.Application.Common.Exceptions;
using EXE201_BE_ThrivoHR.Application.Common.Method;
using EXE201_BE_ThrivoHR.Application.Model;
using EXE201_BE_ThrivoHR.Domain.Entities.Contracts;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace EXE201_BE_ThrivoHR.Application.UseCase.V1.Contracts.EmployeeContracts.Commands;

public record CreateEmployeeContract(EmployeeContractModel EmployeeContractModel) : ICommand;
internal sealed class CreateEmployeeContractHandler(IEmployeeContractRepository employeeContractRepository, IEmployeeRepository employeeRepository, IMapper mapper) : ICommandHandler<CreateEmployeeContract>
{

    private readonly IEmployeeContractRepository _employeeContractRepository = employeeContractRepository;
    private readonly IEmployeeRepository _employeeRepository = employeeRepository;
    private readonly IMapper _mapper = mapper;

    public async Task<Result> Handle(CreateEmployeeContract request, CancellationToken cancellationToken)
    {
        // Check if the employee exists
        var Employee = await _employeeRepository.FindAsync(x => x.EmployeeId == EmployeesMethod.ConvertEmployeeCodeToId(request.EmployeeContractModel.EmployeeCode), cancellationToken) ?? throw new Employee.NotFoundException(request.EmployeeContractModel.EmployeeCode);


        // check if the employee has an existing contract
        var EmployeeCurrentContract = await _employeeContractRepository.FindAsync(x => x.EmployeeId == Employee.Id, cancellationToken);
        if (EmployeeCurrentContract != null && !EmployeeCurrentContract.IsEnded)
        {
            throw new EmployeeContractExceptions.CurrentContractNotEndedException("Employee Contract", request.EmployeeContractModel.EmployeeCode);
        }
        var contract = _mapper.Map<Domain.Entities.Contracts.EmployeeContract>(request.EmployeeContractModel);
        await _employeeContractRepository.AddAsync(contract);
        contract.EmployeeId = Employee.Id;
        if (contract.EndDate is not null)
        {
            contract.Duration = (contract.EndDate.Value.Year - contract.StartDate.Year) * 12;

        }
        else
        {

            contract.IsNoExpiry = true;
        }

        try
        {
            await _employeeContractRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
        }
        catch (DbUpdateException ex)//Handle dupplication exception
        {

            throw new EmployeeContractExceptions.DuplicateException(nameof(Domain.Entities.Contracts.EmployeeContract), ExceptionMethod.GetKeyString(ex.ToString()));
        }
        catch (Exception)
        {
            throw new EmployeeContractExceptions.CreateFailureException(nameof(Domain.Entities.Contracts.EmployeeContract));
        }

        return Result.Success();




    }

}
public sealed class CreateEmployeeContractValidator : AbstractValidator<CreateEmployeeContract>
{
    private readonly IDepartmentRepository _departmentRepository;
    private readonly IPositionRepository _positionRepository;

    public CreateEmployeeContractValidator(IDepartmentRepository departmentRepository, IPositionRepository positionRepository)
    {
        _departmentRepository = departmentRepository;
        _positionRepository = positionRepository;

        RuleFor(x => x.EmployeeContractModel.EmployeeCode)
            .Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage("Employee Code is required");

        RuleFor(x => x.EmployeeContractModel.StartDate)
            .Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage("Start Date is required")
            .GreaterThanOrEqualTo(DateOnly.FromDateTime(DateTime.Today))
            .WithMessage("Start Date must be greater than or equal to today");

        RuleFor(x => x.EmployeeContractModel.DepartmentId)
            .Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage("Department ID is required")
            .MustAsync(ValidateDepartment).WithMessage("Department does not exist");

        RuleFor(x => x.EmployeeContractModel.PositionId)
            .Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage("Position ID is required")
            .MustAsync(ValidatePosition).WithMessage("Position does not exist");
    }

    private async Task<bool> ValidateDepartment(int departmentId, CancellationToken cancellationToken)
    {
        var department = await _departmentRepository.FindAsync(x => x.Id == departmentId, cancellationToken);
        return department != null;
    }

    private async Task<bool> ValidatePosition(int positionId, CancellationToken cancellationToken)
    {
        var position = await _positionRepository.FindAsync(x => x.Id == positionId, cancellationToken);
        return position != null;
    }
}