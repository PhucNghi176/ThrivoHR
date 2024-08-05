using EXE201_BE_ThrivoHR.Application.UseCase.V1.Users.Queries;
using Microsoft.AspNetCore.Mvc;

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
    public async Task<IActionResult> GetEmployee([FromQuery] int PageNumber,int PageSize)
    {
        var result = await _sender.Send(new GetEmployees(PageNumber,PageSize));
        return result.IsSuccess ? Ok(result.Value) : NotFound();
    }
}
