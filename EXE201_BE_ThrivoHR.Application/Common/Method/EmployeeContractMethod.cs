using EXE201_BE_ThrivoHR.Application.Model;
using EXE201_BE_ThrivoHR.Domain.Entities.Contracts;

namespace EXE201_BE_ThrivoHR.Application.Common.Method;

public static class EmployeeContractMethod
{
    public static EmployeeContract CalculateEndDateAndDurationAndExpiryContract(EmployeeContract contract)
    {
        if (contract.EndDate is not null)
        {
            contract.Duration = (contract.EndDate.Value.Year - contract.StartDate.Year) * 12;
        }
        else
        {

            contract.IsNoExpiry = true;
        }
        return contract;
    }
    public static EmployeeContract CalculateEndDateAndDurationAndExpiryContract(EmployeeContract contract, EmployeeContractModelUpdate employeeContractModelUpdate)
    {
        if (employeeContractModelUpdate.EndDate is not null)
        {
            contract.Duration = (employeeContractModelUpdate.EndDate!.Value.Year - employeeContractModelUpdate.StartDate.Year) * 12;
        }
        else
        {
            contract.Duration = 0;
            contract.IsNoExpiry = true;
        }
        return contract;
    }

    public static EmployeeContract EndedContract(EmployeeContract contract)
    {
        contract.IsEnded = true;
        contract.DateEnded = DateOnly.FromDateTime(DateTime.Now);
        return contract;
    }
}
