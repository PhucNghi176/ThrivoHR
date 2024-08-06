using EXE201_BE_ThrivoHR.Application.UseCase.V1.Users.Commands;
using EXE201_BE_ThrivoHR.Application.UseCase.V1.Users.Queries;
using EXE201_BE_ThrivoHR.Domain.Entities.Identity;

namespace EXE201_BE_ThrivoHR.API.Controllers.Users;


public class Employee : BaseController
{
    public Employee(ISender sender) : base(sender)
    {
    }


    [HttpGet("[action]")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<List<AppUser>>> GetEmployee([FromQuery] int PageNumber, int PageSize)
    {
        var result = await _sender.Send(new GetEmployees(PageNumber, PageSize));
        return result;
    }
    [HttpGet("[action]")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<List<AppUser>>> GetEmployeeAsNoTracking([FromQuery] int PageNumber, int PageSize)
    {
        var result = await _sender.Send(new GetEmployeeNoTracking(PageNumber, PageSize));
        return result;
    }








    [HttpGet("generate")]
    [EndpointSummary("Đừng chạy API này nha Đạt")]
    public async Task<IActionResult> GenerateEmployee()
    {
        await _sender.Send(new GenerateEmployeesCommand());
        return NoContent();
    }
}
