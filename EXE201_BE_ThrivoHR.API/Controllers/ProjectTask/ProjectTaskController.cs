using EXE201_BE_ThrivoHR.Application.UseCase.V1.ProjectTasks.Commands;

namespace EXE201_BE_ThrivoHR.API.Controllers.ProjectTask;


public class ProjectTaskController(ISender sender) : BaseController(sender)
{
    [HttpPost]
    public async Task<IActionResult> Create(Create command)
    {
        return Ok(await sender.Send(command));
    }
}
