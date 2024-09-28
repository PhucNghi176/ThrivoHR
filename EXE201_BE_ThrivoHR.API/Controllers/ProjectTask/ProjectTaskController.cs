using EXE201_BE_ThrivoHR.Application.UseCase.V1.ProjectTasks.Commands;
using EXE201_BE_ThrivoHR.Application.UseCase.V1.ProjectTasks.Queries;

namespace EXE201_BE_ThrivoHR.API.Controllers.ProjectTask;


public class ProjectTaskController(ISender sender) : BaseController(sender)
{
    [HttpPost]
    public async Task<IActionResult> Create(Create command)
    {
        return Ok(await sender.Send(command));
    }
    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] Filter query)
    {
        return Ok(await _sender.Send(query));
    }
}

