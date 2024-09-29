
using EXE201_BE_ThrivoHR.Application.UseCase.V1.TaskHistories.Queries;

namespace EXE201_BE_ThrivoHR.API.Controllers.TaskHistory;


public class TaskHistoryController(ISender sender) : BaseController(sender)
{
    [HttpGet]
    public async Task<IActionResult> GetTaskHistory([FromQuery] Filter filter)
    {
        return Ok(await sender.Send(filter));
    }
}
