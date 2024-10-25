using Microsoft.AspNetCore.Http;

namespace EXE201_BE_ThrivoHR.Application.UseCase.FaceDetection;
public record FaceRegister(string EmployeeCode, IFormFile Image) : ICommand<string>;

internal sealed class FaceRegisterHandler : ICommandHandler<FaceRegister, string>
{
    public async Task<Result<string>> Handle(FaceRegister request, CancellationToken cancellationToken)
    {
        using var httpClient = new HttpClient();
        // Prepare the content for the POST request
        using var content = new MultipartFormDataContent();
        // Load the image into a stream
        using var stream = new MemoryStream();
        await request.Image.CopyToAsync(stream, cancellationToken);
        stream.Position = 0;
        var imageContent = new StreamContent(stream);
        imageContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(request.Image.ContentType);
        // Add the image to the form data
        content.Add(imageContent, "image", request.Image.FileName);
        // Send the POST request to the face detection API
        var response = await httpClient.PostAsync(
            $"https://owl-touched-slug.ngrok-free.app/api/v1/face-recognition?employeeCode={request.EmployeeCode}",
            content, cancellationToken);
        if (!response.IsSuccessStatusCode)
            throw new Exception();

        var result = await response.Content.ReadAsStringAsync(cancellationToken);
        return Result.Success(result);
    }
}