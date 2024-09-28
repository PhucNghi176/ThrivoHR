
using EXE201_BE_ThrivoHR.Application.Common.Exceptions;
using EXE201_BE_ThrivoHR.Application.Common.Method;
using EXE201_BE_ThrivoHR.Application.Model;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace EXE201_BE_ThrivoHR.Application.UseCase.V1.Contracts.EmployeeContracts.Commands;

public record CreateEmployeeContract(EmployeeContractModelCreate EmployeeContractModel) : ICommand;
internal sealed class CreateEmployeeContractHandler(IEmployeeContractRepository employeeContractRepository, IEmployeeRepository employeeRepository, IMapper mapper, IDepartmentRepository departmentRepository) : ICommandHandler<CreateEmployeeContract>
{
    public async Task<Result> Handle(CreateEmployeeContract request, CancellationToken cancellationToken)
    {
        // Check if the employee exists
        var Employee = await employeeRepository.FindAsync(x => x.EmployeeId == EmployeesMethod.ConvertEmployeeCodeToId(request.EmployeeContractModel.EmployeeCode), cancellationToken) ?? throw new Employee.NotFoundException(request.EmployeeContractModel.EmployeeCode);

        var HeadDepartment = await departmentRepository.FindAsync(x => x.Id == request.EmployeeContractModel.DepartmentId, cancellationToken);

        // check if the employee has an existing contract
        var EmployeeCurrentContract = await employeeContractRepository.FindAsync(x => x.EmployeeId == Employee.Id, cancellationToken);
        if (EmployeeCurrentContract != null && !EmployeeCurrentContract.IsEnded)
        {
            throw new EmployeeContractExceptions.CurrentContractNotEndedException("Employee Contract", request.EmployeeContractModel.EmployeeCode);
        }
        var contract = mapper.Map<Domain.Entities.Contracts.EmployeeContract>(request.EmployeeContractModel);

        contract.EmployeeId = Employee.Id;
        Employee = EmployeesMethod.SetDepartmentAndPostionForEmployee(Employee, contract);
        Employee.ManagerId = HeadDepartment.HeadOfDepartmentId;
        contract = EmployeeContractMethod.CalculateEndDateAndDurationAndExpiryContract(contract);
        try
        {
            await Task.WhenAll(
                    employeeRepository.UpdateAsync(Employee),
                    employeeContractRepository.AddAsync(contract));
            await employeeContractRepository.UnitOfWork.SaveChangesAsync(cancellationToken);

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
            .MustAsync(async (departmentID, cancellation) =>
                await ValidateMethod.DepartmentExsis(departmentID, _departmentRepository))
            .WithMessage("Department does not exist");

        RuleFor(x => x.EmployeeContractModel.PositionId)
            .Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage("Position ID is required")
            .MustAsync(async (positionID, cancellation) =>
                await ValidateMethod.PositionExsis(positionID, _positionRepository))
            .WithMessage("Position does not exist");
    }
}