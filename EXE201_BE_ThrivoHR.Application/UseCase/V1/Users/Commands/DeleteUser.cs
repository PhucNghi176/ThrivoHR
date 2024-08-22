
using EXE201_BE_ThrivoHR.Application.Common.Method;
using EXE201_BE_ThrivoHR.Application.Common.Security;
using EXE201_BE_ThrivoHR.Domain.Services;
using static EXE201_BE_ThrivoHR.Application.Common.Exceptions.Employee;

namespace EXE201_BE_ThrivoHR.Application.UseCase.V1.Users.Commands;

[Authorize]
public record DeleteUser(string EmployeeCode) : ICommand<string>;

internal sealed class DeleteUserCommandHandler(IEmployeeRepository userRepository) : ICommandHandler<DeleteUser, string>
{
    public async Task<Result<string>> Handle(DeleteUser request, CancellationToken cancellationToken)
    {
        var user = await userRepository.FindAsync(x => x.EmployeeId == EmployeesMethod.ConvertEmployeeCodeToId(request.EmployeeCode), cancellationToken) ?? throw new NotFoundException(request.EmployeeCode);
        user.IsDeleted = true;
        await userRepository.UpdateAsync(user);
        await userRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
        return Result.Success($"Delete employee {user.EmployeeCode} successfully");
    }
}

