using Microsoft.AspNetCore.Http;

namespace EXE201_BE_ThrivoHR.Domain.Repositories;
public interface IFaceDetectionRepository
{
    Task<string> DetectFaceFromImage(IFormFile image, string[] directories);

    Task<string> SaveImage(IFormFile image, string employeeId, string employeeFolderPath,
        CancellationToken cancellationToken = default);
}