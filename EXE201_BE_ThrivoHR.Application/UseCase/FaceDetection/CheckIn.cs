using EXE201_BE_ThrivoHR.Domain.Common.Exceptions;
using EXE201_BE_ThrivoHR.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;

namespace EXE201_BE_ThrivoHR.Application.UseCase.FaceDetection;
public record CheckIn(IFormFile Image, bool IsCheckIn) : ICommand<string>;

internal sealed class DetectFaceFromImageHandler(
    IAttendanceRepository attendanceRepository,
    IEmployeeRepository employeeRepository) : ICommandHandler<CheckIn, string>
{
    public async Task<Result<string>> Handle(CheckIn request, CancellationToken cancellationToken)
    {
        using var httpClient = new HttpClient();
        // Prepare the content for the POST request
        using var content = new MultipartFormDataContent();
        // Load the image into a stream
        using var stream = new MemoryStream();
        await request.Image.CopyToAsync(stream, cancellationToken);
        stream.Position = 0;

        var imageContent = new StreamContent(stream);
        imageContent.Headers.ContentType =
            new System.Net.Http.Headers.MediaTypeHeaderValue(request.Image.ContentType);

        // Add the image to the form data
        content.Add(imageContent, "image", request.Image.FileName);

        // Send the POST request to the face detection API
        var response = await httpClient.PostAsync("https://owl-touched-slug.ngrok-free.app/api/v1/face-recognition/detect",
            content, cancellationToken);

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception();
        }

        // Read the response content
        var result = await response.Content.ReadAsStringAsync(cancellationToken);
        var employee = await employeeRepository.FindAsync(x => x.EmployeeId == Int16.Parse(result), cancellationToken);
        if (employee == null)
        {
            throw new NotFoundException("Employee not recognized");
        }

        var Today = DateOnly.FromDateTime(DateTime.Today);
        var IsAlreadyCheckIn =
            await attendanceRepository.FindAsync(x => x.EmployeeId == employee.Id && x.Date == Today,
                cancellationToken);
        var IsCheckIn = request.IsCheckIn ? "Checked-in" : "Checked-out";
        if (IsAlreadyCheckIn != null)
        {
            IsAlreadyCheckIn.CheckOut = TimeOnly.FromDateTime(DateTime.UtcNow.AddHours(7));
            await attendanceRepository.UpdateAsync(IsAlreadyCheckIn);
        }
        else
        {
            var attendance = new Attendance
            {
                CheckIn = TimeOnly.FromDateTime(DateTime.UtcNow.AddHours(7)),
                Date = Today,
                EmployeeId = employee.Id,
                Note = IsCheckIn,
            };
            await attendanceRepository.AddAsync(attendance);
        }

        await attendanceRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
        return $"Employee {employee.EmployeeCode} {IsCheckIn} successfully";

        // Handle error response
    }
}