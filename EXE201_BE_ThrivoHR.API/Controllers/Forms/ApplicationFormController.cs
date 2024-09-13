using EXE201_BE_ThrivoHR.Application.UseCase.V1.Forms.ApplicationForms.Queries;
using EXE201_BE_ThrivoHR.Application.UseCase.V1.Forms.ApplicationForms.Commands;

namespace EXE201_BE_ThrivoHR.API.Controllers.Forms;

public class ApplicationFormController(ISender sender) : BaseController(sender)
{
    [HttpPost]
    public async Task<IActionResult> CreateApplicationForm([FromBody] CreateApplicationForms request)
    {
        return Ok(await _sender.Send(request));
    }
    [HttpGet]
    public async Task<IActionResult> GetApplicationForm([FromQuery] FilterApplicationForm request)
    {
        return Ok(await _sender.Send(request));
    }
    [HttpPut("update-status")]
    public async Task<IActionResult> UpdateStatusApplicationForm([FromQuery] ChangeStatusApplicationForm request)
    {
        return Ok(await _sender.Send(request));
    }
}
