using EXE201_BE_ThrivoHR.Application.UseCase.V1.Projects.Commands;
using EXE201_BE_ThrivoHR.Application.UseCase.V1.Projects.Queries;

namespace EXE201_BE_ThrivoHR.API.Controllers.Project;


public class ProjectController(ISender sender) : BaseController(sender)
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
    [HttpPut("change-status")]
    public async Task<IActionResult> ChangeStatus([FromQuery] ChangeStatus command)
    {
        return Ok(await _sender.Send(command));
    }
    [HttpPut]
    public async Task<IActionResult> Update(UpdateProject command)
    {
        return Ok(await _sender.Send(command));
    }
    [HttpPut("edit-member")]
    public async Task<IActionResult> GetProjectDetail([FromQuery] EditMemberInProject command)
    {
        return Ok(await _sender.Send(command));
    }
    [HttpDelete]
    public async Task<IActionResult> Delete([FromQuery] Delete command)
    {
        return Ok(await _sender.Send(command));
    }
}
