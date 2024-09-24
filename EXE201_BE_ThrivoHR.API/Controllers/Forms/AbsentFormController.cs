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

}
