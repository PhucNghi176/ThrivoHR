using EXE201_BE_ThrivoHR.Application.UseCase.V1.Overtimes.Commands;
using EXE201_BE_ThrivoHR.Application.UseCase.V1.Overtimes.Queries;

namespace EXE201_BE_ThrivoHR.API.Controllers.Overtimes;
public class OvertimeController(ISender sender) : BaseController(sender)
{
    [HttpPost]
    public async Task<IActionResult> CreateOvertime([FromBody] CreateOvertime command)
    {
        return Ok(await sender.Send(command));
    }
    [HttpGet]
    public async Task<IActionResult> GetOvertimes([FromQuery] Fitler query)
    {
        return Ok(await sender.Send(query));
    }
}
