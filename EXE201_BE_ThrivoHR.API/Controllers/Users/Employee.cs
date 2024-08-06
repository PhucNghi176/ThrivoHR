using EXE201_BE_ThrivoHR.Application.UseCase.V1.Users.Commands;
using EXE201_BE_ThrivoHR.Application.UseCase.V1.Users.Queries;

namespace EXE201_BE_ThrivoHR.API.Controllers.Users;


public class Employee : BaseController
{
    public Employee(ISender sender) : base(sender)
    {
    }
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetEmployee([FromQuery] FilterEmployee filterEmployee)
    {
        var result = await _sender.Send(filterEmployee);
        return result.IsSuccess ? Ok(result.Value) : NotFound();
    }
    [HttpGet("generate")]
    [EndpointSummary("Đừng chạy API này nha Đạt")]
    public async Task<IActionResult> GenerateEmployee()
    {
        await _sender.Send(new GenerateEmployeesCommand());
        return NoContent();
    }
}
