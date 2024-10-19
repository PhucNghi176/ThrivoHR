using EXE201_BE_ThrivoHR.Application.UseCase.FaceDetection;

namespace EXE201_BE_ThrivoHR.API.Controllers.FaceDetection;
public class Face(ISender sender) : BaseController(sender)
{
    [HttpPost("detect-image")]
    public async Task<IActionResult> DetectFaceFromImage([FromForm] DetectFaceFromImage request)
    {
        var result = await sender.Send(request);
        return Ok(result);
    }
    [HttpPost]
    public async Task<IActionResult> SaveImage([FromForm] SaveImage request)
    {
        var result = await sender.Send(request);
        return Ok(result);
    }
}