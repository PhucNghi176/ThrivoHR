using EXE201_BE_ThrivoHR.Application.UseCase.V1.Contracts.EmployeeContracts;
using EXE201_BE_ThrivoHR.Application.UseCase.V1.Contracts.EmployeeContracts.Commands;
using EXE201_BE_ThrivoHR.Application.UseCase.V1.Contracts.EmployeeContracts.Queries;
using Marvin.Cache.Headers;

namespace EXE201_BE_ThrivoHR.API.Controllers.Contracts;
public class EmployeeContract(ISender sender) : BaseController(sender)
{
    [HttpPost]
    public async Task<ActionResult<Result>> CreateEmployeeContract(CreateEmployeeContract employeeContractDto, CancellationToken cancellationToken)
    {
        var result = await _sender.Send(employeeContractDto, cancellationToken);
        return result;
    }

    [HttpGet]
    public async Task<ActionResult<Result<PagedResult<EmployeeContractDto>>>> GetEmployeeContracts([FromQuery] FilterEmployeeContract filterEmployeeContract, CancellationToken cancellationToken)
    {
        var result = await _sender.Send(filterEmployeeContract, cancellationToken);
        return result.IsSuccess ? result : BadRequest(result.Error);
    }

    [HttpGet("generate")]
    [HttpCacheIgnore]
    [EndpointSummary("Đừng chạy API này nha Đạt")]
    public async Task<IActionResult> GetEmployeeContractById(CancellationToken cancellationToken)
    {
        var result = await _sender.Send(new GenerateEmployeeContract(), cancellationToken);
        return Ok(result);
    }

}
