using EXE201_BE_ThrivoHR.Application.Model;
using EXE201_BE_ThrivoHR.Application.UseCase.Authentication;
using EXE201_BE_ThrivoHR.Domain.Services;

namespace EXE201_BE_ThrivoHR.API.Controllers.Authenticate;
public class Authentication(ISender sender, ICurrentUserService currentUserService) : BaseController(sender)
{
   
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Authenticate([FromBody] LoginQuery loginQuery)
    {
        var result = await _sender.Send(new LoginQuery(loginQuery.EmployeeCode, loginQuery.Password));
        return Ok(result);
    }
    [HttpPost("refresh")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> RefreshToken([FromBody] TokenModel tokenModel)
    {
        var result = await _sender.Send(new RefreshTokenQuery(tokenModel));
        return Ok(result);
    }
    [HttpGet]
    public IActionResult Get()
    {
       return Ok(currentUserService.IP);
    }
}
