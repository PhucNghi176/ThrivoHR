using EXE201_BE_ThrivoHR.Application.Common.Exceptions;

namespace EXE201_BE_ThrivoHR.Application.UseCase.V1.Contracts.EmployeeContracts.Commands;

public record DeleteContract(string ContractID) : ICommand;
internal sealed class DeleteContractHandler(IEmployeeContractRepository employeeContractRepository, IEmployeeRepository employeeRepository) : ICommandHandler<DeleteContract>
{

    private readonly IEmployeeContractRepository _employeeContractRepository = employeeContractRepository;
    private readonly IEmployeeRepository _employeeRepository = employeeRepository;

    public async Task<Result> Handle(DeleteContract request, CancellationToken cancellationToken)
    {

        var EmployeeCurrentContract = await _employeeContractRepository.FindAsync(x => x.Id == request.ContractID, cancellationToken) ?? throw new EmployeeContractExceptions.NotFoundException(request.ContractID);
        // Check if the employee exists
        _ = await _employeeRepository.FindAsync(x => x.Id == EmployeeCurrentContract.EmployeeId, cancellationToken) ?? throw new EmployeeContractExceptions.ContractWithEmployeeNotFoundException();


        // check if the employee has an existing contract

        EmployeeCurrentContract.IsDeleted = true;
        await _employeeContractRepository.UpdateAsync(EmployeeCurrentContract);
        await _employeeContractRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
        return Result.Success();


    }
}

