using EXE201_BE_ThrivoHR.Application.UseCase.V1.Projects.Commands;

namespace EXE201_BE_ThrivoHR.API.Controllers.Project;


public class ProjectController(ISender sender) : BaseController(sender)
{
    [HttpPost]
    public async Task<IActionResult> Create(Create command)
    {
        return Ok(await sender.Send(command));
    }
}
