using EXE201_BE_ThrivoHR.Domain.Entities.Contracts;

namespace EXE201_BE_ThrivoHR.Application.UseCase.V1.Contracts;

public static class EmployeeContractMappingExstension
{
    public static EmployeeContractDto MapToEmployeeContractDto(this EmployeeContract employeeContract, IMapper mapper)
    {
        return mapper.Map<EmployeeContractDto>(employeeContract);
    }

    public static List<EmployeeContractDto> MapToEmployeeContractListDto(this IEnumerable<EmployeeContract> employeeContracts, IMapper mapper)
    {
        return employeeContracts.Select(employeeContract => employeeContract.MapToEmployeeContractDto(mapper)).ToList();
    }
}
