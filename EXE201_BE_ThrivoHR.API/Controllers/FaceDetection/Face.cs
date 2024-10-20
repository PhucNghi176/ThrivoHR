using EXE201_BE_ThrivoHR.Application.UseCase.FaceDetection;

namespace EXE201_BE_ThrivoHR.API.Controllers.FaceDetection;
public class Face(ISender sender) : BaseController(sender)
{
    [HttpPost]
    public async Task<IActionResult> DetectFaceFromImage([FromForm] CheckIn request)
    {
        var result = await sender.Send(request);
        return Ok(result);
    }
    [HttpGet]
    public async Task<IActionResult> GetAttendance([FromQuery] Filter request)
    {
        var result = await sender.Send(request);
        return Ok(result);
    }
   
}