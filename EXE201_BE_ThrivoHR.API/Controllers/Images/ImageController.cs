using EXE201_BE_ThrivoHR.Application.UseCase.V1.Images.Commands;

namespace EXE201_BE_ThrivoHR.API.Controllers.Images;


public class ImageController(ISender sender) : BaseController(sender)
{
    [HttpPost]
    public async Task<IActionResult> Upload([FromForm] Upload upload)
    {
        var result = await _sender.Send(upload);
        return Ok(result);
    }
}
