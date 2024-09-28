using EXE201_BE_ThrivoHR.Application.Common.Exceptions;
using EXE201_BE_ThrivoHR.Application.Common.Method;
using EXE201_BE_ThrivoHR.Application.Model;

namespace EXE201_BE_ThrivoHR.Application.UseCase.V1.Contracts.EmployeeContracts.Commands;

public record UpdateEmployeeContract(EmployeeContractModelUpdate EmployeeContractModel) : ICommand;

internal sealed class UpdateEmployeeContractHandler(IEmployeeContractRepository employeeContractRepository, IEmployeeRepository employeeRepository, IMapper mapper) : ICommandHandler<UpdateEmployeeContract>
{

    private readonly IEmployeeContractRepository _employeeContractRepository = employeeContractRepository;
    private readonly IEmployeeRepository _employeeRepository = employeeRepository;
    private readonly IMapper _mapper = mapper;

    public async Task<Result> Handle(UpdateEmployeeContract request, CancellationToken cancellationToken)
    {

        var ContractCorresponseEmployee = await _employeeContractRepository.FindAsync(x => x.Id == request.EmployeeContractModel.ContractId, cancellationToken) ?? throw new EmployeeContractExceptions.NotFoundException(request.EmployeeContractModel.ContractId);
        // Check if the employee exists
        var Employee = await _employeeRepository.FindAsync(x => x.Id == ContractCorresponseEmployee.EmployeeId, cancellationToken) ?? throw new EmployeeContractExceptions.ContractWithEmployeeNotFoundException();
        Employee = EmployeesMethod.SetDepartmentAndPostionForEmployee(Employee, ContractCorresponseEmployee);
        ContractCorresponseEmployee = EmployeeContractMethod.CalculateEndDateAndDurationAndExpiryContract(ContractCorresponseEmployee, request.EmployeeContractModel);
        _mapper.Map(request.EmployeeContractModel, ContractCorresponseEmployee);
        await _employeeContractRepository.UpdateAsync(ContractCorresponseEmployee);
        await _employeeRepository.UpdateAsync(Employee);
        await _employeeContractRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
        return Result.Success();

    }
}
