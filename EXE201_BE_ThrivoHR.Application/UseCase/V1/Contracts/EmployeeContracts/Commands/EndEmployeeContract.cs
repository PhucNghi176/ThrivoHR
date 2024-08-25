using EXE201_BE_ThrivoHR.Application.Common.Exceptions;
using EXE201_BE_ThrivoHR.Application.Common.Method;

namespace EXE201_BE_ThrivoHR.Application.UseCase.V1.Contracts.EmployeeContracts.Commands;

public record EndEmployeeContract(string ContractID) : ICommand;
internal sealed class EndEmployeeContractHandler(IEmployeeContractRepository employeeContractRepository, IEmployeeRepository employeeRepository) : ICommandHandler<EndEmployeeContract>
{

    private readonly IEmployeeContractRepository _employeeContractRepository = employeeContractRepository;
    private readonly IEmployeeRepository _employeeRepository = employeeRepository;

    public async Task<Result> Handle(EndEmployeeContract request, CancellationToken cancellationToken)
    {

        var EmployeeCurrentContract = await _employeeContractRepository.FindAsync(x => x.Id == request.ContractID, cancellationToken) ?? throw new EmployeeContractExceptions.NotFoundException(request.ContractID);
        if (EmployeeCurrentContract.IsEnded) throw new EmployeeContractExceptions.ContractAlreadyEndedException(request.ContractID);
        // Check if the employee exists
        var Employee = await _employeeRepository.FindAsync(x => x.Id == EmployeeCurrentContract.EmployeeId, cancellationToken) ?? throw new EmployeeContractExceptions.ContractWithEmployeeNotFoundException();
        EmployeeCurrentContract = EmployeeContractMethod.EndedContract(EmployeeCurrentContract);
        Employee = EmployeesMethod.SetDepartmentAndPostionForEmployee(Employee);
        await _employeeContractRepository.UpdateAsync(EmployeeCurrentContract);
        await _employeeRepository.UpdateAsync(Employee);
        await _employeeContractRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
        return Result.Success();


    }

}
