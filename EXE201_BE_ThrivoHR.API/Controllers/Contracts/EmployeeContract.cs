
using EXE201_BE_ThrivoHR.Application.Common.Models;
using EXE201_BE_ThrivoHR.Application.UseCase.V1.Contracts.EmployeeContracts.Commands;

namespace EXE201_BE_ThrivoHR.API.Controllers.Contracts;
public class EmployeeContract(ISender sender) : BaseController(sender)
{
    [HttpPost]
    public async Task<ActionResult<Result>> CreateEmployeeContract(CreateEmployeeContract employeeContractDto, CancellationToken cancellationToken)
    {
        var result = await _sender.Send(employeeContractDto, cancellationToken);
        return result;
    }



}
