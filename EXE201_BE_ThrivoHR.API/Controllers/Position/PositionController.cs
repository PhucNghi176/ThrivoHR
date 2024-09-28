
using EXE201_BE_ThrivoHR.Application.UseCase.V1.Positions.Queries;

namespace EXE201_BE_ThrivoHR.API.Controllers.Position;


public class PositionController(ISender sender) : BaseController(sender)
{
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var result = await _sender.Send(new Get());
        return Ok(result);
    }
}
