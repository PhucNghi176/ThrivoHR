using EXE201_BE_ThrivoHR.Application.Common.Exceptions;
using EXE201_BE_ThrivoHR.Application.Common.Method;

namespace EXE201_BE_ThrivoHR.Application.UseCase.Authentication;

public record ChangePassword(string EmployeeCode, string OldPassword, string NewPassword) : ICommand;
internal sealed class ChangePasswordHandler(IEmployeeRepository _employeeRepository) : ICommandHandler<ChangePassword>
{
    public async Task<Result> Handle(ChangePassword request, CancellationToken cancellationToken)
    {
        var Employee = await _employeeRepository.FindAsync(x => x.EmployeeId == EmployeesMethod.ConvertEmployeeCodeToId(request.EmployeeCode), cancellationToken) ?? throw new Employee.NotFoundException(request.EmployeeCode);
        Employee.PasswordHash ??= BCrypt.Net.BCrypt.HashPassword(request.NewPassword);
        if (!await _employeeRepository.VerifyPasswordAsync(request.OldPassword, Employee.PasswordHash!))
        {
            throw new Employee.PasswordMismatchException();
        }
        Employee.PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.NewPassword);
        await _employeeRepository.UpdateAsync(Employee);
        await _employeeRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
        return Result.Success();


    }
}
