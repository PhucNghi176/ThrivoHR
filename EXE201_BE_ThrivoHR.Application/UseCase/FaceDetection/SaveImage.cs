using EXE201_BE_ThrivoHR.Application.Common.Method;
using Microsoft.AspNetCore.Http;

namespace EXE201_BE_ThrivoHR.Application.UseCase.FaceDetection;
public record SaveImage (IFormFile Image,string EmployeeCode): ICommand<string>;
internal sealed class SaveImageHandler(IFaceDetectionRepository faceDetectionService)
    : ICommandHandler<SaveImage, string>
{
    private readonly string _rootPath = Directory.GetCurrentDirectory();
    public async Task<Result<string>> Handle(SaveImage request, CancellationToken cancellationToken)
    {
        var employeeFolderPath = Path.Combine(_rootPath, "TrainedFaces", EmployeesMethod.ConvertEmployeeCodeToId(request.EmployeeCode).ToString());
        var result = await faceDetectionService.SaveImage(request.Image, request.EmployeeCode, employeeFolderPath, cancellationToken);
        return Result.Success(result);
    }
}