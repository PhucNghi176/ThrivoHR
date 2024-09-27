using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using EXE201_BE_ThrivoHR.Application.Common.Method;
using Microsoft.AspNetCore.Http;
using Error = EXE201_BE_ThrivoHR.Application.Common.Models.Error;

namespace EXE201_BE_ThrivoHR.Application.UseCase.V1.Images.Commands;

public record Upload(IFormFile FormFile, string EmployeeCode) : ICommand<string>;
internal sealed class UploadHandler : ICommandHandler<Upload, string>
{
    private readonly IEmployeeRepository _employeeRepository;
    private readonly Cloudinary cloudinary;

    public UploadHandler(IEmployeeRepository employeeRepository, Cloudinary cloudinary)
    {
        _employeeRepository = employeeRepository;
        this.cloudinary = cloudinary;
    }

    public async Task<Result<string>> Handle(Upload request, CancellationToken cancellationToken)
    {
        var upload = new ImageUploadParams()
        {
            File = new FileDescription(request.EmployeeCode, request.FormFile.OpenReadStream()),
            DisplayName = request.EmployeeCode,
            Overwrite = true,
            Transformation = new Transformation().Width(400).Height(400).Crop("fill")          
           
        };
        var result = cloudinary.UploadAsync(upload);
        if (result.Result.StatusCode != System.Net.HttpStatusCode.OK)
        {
            return (Result<string>)Result.Failure(Error.UploadFail);
        }
        else
        {
            var Employee = await _employeeRepository.FindAsync(x => x.EmployeeId == EmployeesMethod.ConvertEmployeeCodeToId(request.EmployeeCode), cancellationToken) ?? throw new Common.Exceptions.Employee.NotFoundException(request.EmployeeCode);
            Employee.ImageUrl = result.Result.SecureUrl.AbsoluteUri;
            await _employeeRepository.UpdateAsync(Employee);
            await _employeeRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
        }
        return Result.Success("Upload Success");

    }
}
