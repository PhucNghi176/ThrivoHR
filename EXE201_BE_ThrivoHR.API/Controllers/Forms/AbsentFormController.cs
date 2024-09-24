using EXE201_BE_ThrivoHR.Application.UseCase.V1.Forms.AbsentForms.Commands;
using EXE201_BE_ThrivoHR.Application.UseCase.V1.Forms.AbsentForms.Queries;

namespace EXE201_BE_ThrivoHR.API.Controllers.Forms;
public class AbsentFormController(ISender sender) : BaseController(sender)
{
    [HttpPost]
    public async Task<IActionResult> CreateAbsentForm([FromBody] CreateAbsentForm command)
    {
        return Ok(await _sender.Send(command));
    }
    [HttpGet]
    public async Task<IActionResult> GetAbsentForm([FromQuery] FilterAbsentForm query)
    {
        return Ok(await _sender.Send(query));
    }
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAbsentForm(string id, [FromBody] UpdateAbsentForm command)
    {
        if (id != command.ID)
        {
            return BadRequest();
        }
        return Ok(await _sender.Send(command));
    }
    [HttpPut("{id}/status")]
    public async Task<IActionResult> UpdateStatusAbsentForm(string id, [FromBody] UpdateStatusAbsentForm command)
    {
        if (id != command.ID)
        {
            return BadRequest();
        }
        return Ok(await _sender.Send(command));
    }

}
