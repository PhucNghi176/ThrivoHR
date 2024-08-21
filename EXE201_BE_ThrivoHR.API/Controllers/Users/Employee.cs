using EXE201_BE_ThrivoHR.Application.Common.Models;
using EXE201_BE_ThrivoHR.Application.Common.Pagination;
using EXE201_BE_ThrivoHR.Application.UseCase.V1.Users;
using EXE201_BE_ThrivoHR.Application.UseCase.V1.Users.Commands;
using EXE201_BE_ThrivoHR.Application.UseCase.V1.Users.Queries;
using Marvin.Cache.Headers;


namespace EXE201_BE_ThrivoHR.API.Controllers.Users;


public class Employee(ISender sender) : BaseController(sender)
{
    [HttpGet]
    //[ResponseCache(CacheProfileName ="120")]
    [HttpCacheExpiration(CacheLocation = CacheLocation.Private, MaxAge = 300)]
    [HttpCacheValidation(MustRevalidate = true)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Result<PagedResult<EmployeeDto>>>> GetEmployee([FromQuery] FilterEmployee filterEmployee, CancellationToken cancellationToken = default)
    {
        var result = await _sender.Send(filterEmployee, cancellationToken);
        return result.IsSuccess ? Ok(result) : NotFound();
    }
    [HttpPost]
    [HttpCacheValidation(MustRevalidate = true)]
    [HttpCacheExpiration(CacheLocation = CacheLocation.Private, MaxAge = 60)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Result>> CreateEmployee([FromBody] CreateUser createUser, CancellationToken cancellationToken)
    {
        var result = await _sender.Send(createUser, cancellationToken);
        return result.IsSuccess ? CreatedAtAction(nameof(GetEmployee), result) : BadRequest(result);
    }

    [HttpPut]

    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Result>> UpdateEmployee([FromBody] UpdateUser updateUser, CancellationToken cancellationToken)
    {
        var result = await _sender.Send(updateUser, cancellationToken);
        return result.IsSuccess ? Ok(result) : BadRequest(result);
    }
    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Result<string>>> DeleteEmployee([FromBody] DeleteUser deleteUser, CancellationToken cancellationToken)
    {
        var result = await _sender.Send(deleteUser, cancellationToken);
        return result.IsSuccess ? Ok(result) : BadRequest(result);
    }








    [HttpGet("generate")]
    [EndpointSummary("Đừng chạy API này nha Đạt")]
    public async Task<IActionResult> GenerateEmployee()
    {
        await _sender.Send(new GenerateEmployeesCommand());
        return NoContent();
    }
}
