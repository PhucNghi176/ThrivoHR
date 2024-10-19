using Microsoft.AspNetCore.Http;

namespace EXE201_BE_ThrivoHR.Application.UseCase.FaceDetection;
public record DetectFaceFromImage(IFormFile Image, DateTime CheckInTime) : ICommand<string>;
internal sealed class DetectFaceFromImageHandler(IFaceDetectionRepository faceDetectionService)
    : ICommandHandler<DetectFaceFromImage, string>
{
    private readonly string[] _rootPath = Directory.GetDirectories(Directory.GetCurrentDirectory() + "/TrainedFaces/");

    public async Task<Result<string>> Handle(DetectFaceFromImage request, CancellationToken cancellationToken)
    {

        var result = await faceDetectionService.DetectFaceFromImage(request.Image, _rootPath);
        return Result.Success(result);
    }
}

