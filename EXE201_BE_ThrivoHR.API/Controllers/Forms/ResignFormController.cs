using EXE201_BE_ThrivoHR.Application.UseCase.V1.Forms.ResignForms.Queries;

namespace EXE201_BE_ThrivoHR.API.Controllers.Forms;
public class ResignFormController(ISender sender) : BaseController(sender)
{
    [HttpGet]
    public async Task<IActionResult> GetResignForm([FromQuery] FilterResignForms request)
    {
        return Ok(await _sender.Send(request));
    }
}
