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
    public async Task<IActionResult> GetEmployee([FromQuery] FilterEmployee filterEmployee)
    {
        var result = await _sender.Send(filterEmployee);
        Console.WriteLine("\n\n\n\n\n\n"+DateTimeOffset.Now);
        return result.IsSuccess ? Ok(result.Value) : NotFound();
    }
    [HttpPost]
    [HttpCacheValidation(MustRevalidate = true)]
    [HttpCacheExpiration(CacheLocation = CacheLocation.Private, MaxAge = 60)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateEmployee([FromBody] CreateUser createUser)
    {
        var result = await _sender.Send(createUser);
        return result.IsSuccess ? CreatedAtAction(nameof(GetEmployee),result) : BadRequest();
    }












    [HttpGet("generate")]
    [EndpointSummary("Đừng chạy API này nha Đạt")]
    public async Task<IActionResult> GenerateEmployee()
    {
        await _sender.Send(new GenerateEmployeesCommand());
        return NoContent();
    }
}
