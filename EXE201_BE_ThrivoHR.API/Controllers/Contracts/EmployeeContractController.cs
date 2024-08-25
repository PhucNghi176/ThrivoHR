using EXE201_BE_ThrivoHR.Application.Model;
using EXE201_BE_ThrivoHR.Application.UseCase.V1.Contracts.EmployeeContracts;
using EXE201_BE_ThrivoHR.Application.UseCase.V1.Contracts.EmployeeContracts.Commands;
using EXE201_BE_ThrivoHR.Application.UseCase.V1.Contracts.EmployeeContracts.Queries;
using Marvin.Cache.Headers;

namespace EXE201_BE_ThrivoHR.API.Controllers.Contracts;
public class EmployeeContractController(ISender sender) : BaseController(sender)
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
    [HttpDelete]
    [EndpointSummary("dùng để xóa hợp đồng lao động")]
    public async Task<ActionResult<Result>> DeleteEmployeeContract(string ContractID, DeleteContract deleteContract, CancellationToken cancellationToken)
    {
        if (deleteContract.ContractID != ContractID)
        {
            return Result.Failure(Error.NotFoundID);
        }

        var result = await _sender.Send(deleteContract, cancellationToken);
        return result.IsSuccess ? result : BadRequest(result.IsFailure);
    }
    [HttpPut("end-employee-contract")]
    [EndpointSummary("dùng để chấm dứt hợp đồng lao động")]
    public async Task<ActionResult<Result>> EndEmployeeContract(string ContractID, EndEmployeeContract endEmployeeContract, CancellationToken cancellationToken)
    {
        if (endEmployeeContract.ContractID != ContractID)
        {
            return Result.Failure(Error.NotFoundID);
        }
        var result = await _sender.Send(endEmployeeContract, cancellationToken);
        return result;
    }

    [HttpPut]
    public async Task<ActionResult<Result>> UpdateEmployeeContract(string ContractID, UpdateEmployeeContract updateEmployeeContract, CancellationToken cancellationToken)
    {
        if (updateEmployeeContract.EmployeeContractModel.ContractId != ContractID)
        {
            return Result.Failure(Error.NotFoundID);
        }
        var result = await _sender.Send(updateEmployeeContract, cancellationToken);
        return result;
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
