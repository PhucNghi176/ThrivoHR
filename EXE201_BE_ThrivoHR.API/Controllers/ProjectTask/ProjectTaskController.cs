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
    [HttpPut]
    public async Task<IActionResult> Update(Update command)
    {
        return Ok(await _sender.Send(command));
    }
    [HttpDelete]
    public async Task<IActionResult> Delete([FromQuery] Delete command)
    {
        return Ok(await _sender.Send(command));
    }
    [HttpPut("change-assignee")]
    public async Task<IActionResult> ChangeAssignee([FromQuery] ChangeAssignee command)
    {
        return Ok(await _sender.Send(command));
    }
    [HttpPut("change-status")]
    public async Task<IActionResult> ChangeStatus([FromQuery] ChangeStatus command)
    {
        return Ok(await _sender.Send(command));
    }
    [HttpPut("reset-due-date")]
    public async Task<IActionResult> ResetDueDate([FromQuery] ResetDueDate command)
    {
        return Ok(await _sender.Send(command));
    }

}

