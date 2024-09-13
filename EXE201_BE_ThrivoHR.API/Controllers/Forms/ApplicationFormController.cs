using EXE201_BE_ThrivoHR.Application.UseCase.V1.Forms.ApplicationForms.Commands;

namespace EXE201_BE_ThrivoHR.API.Controllers.Forms;


public class ApplicationFormController(ISender sender) : BaseController(sender)
{
    [HttpPost]
    public async Task<IActionResult> CreateApplicationForm([FromBody] CreateApplicationForms request)
    {
        return Ok(await _sender.Send(request));
    }
}
