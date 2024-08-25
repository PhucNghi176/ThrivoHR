
using EXE201_BE_ThrivoHR.Application.Common.Method;
using EXE201_BE_ThrivoHR.Application.Common.Security;
using static EXE201_BE_ThrivoHR.Application.Common.Exceptions.Employee;

namespace EXE201_BE_ThrivoHR.Application.UseCase.V1.Users.Commands;


public record DeleteUser(string EmployeeCode) : ICommand;

internal sealed class DeleteUserCommandHandler(IEmployeeRepository userRepository) : ICommandHandler<DeleteUser>
{
    public async Task<Result> Handle(DeleteUser request, CancellationToken cancellationToken)
    {
        var user = await userRepository.FindAsync(x => x.EmployeeId == EmployeesMethod.ConvertEmployeeCodeToId(request.EmployeeCode), cancellationToken) ?? throw new NotFoundException(request.EmployeeCode);
        user.IsDeleted = true;
        await userRepository.UpdateAsync(user);
        await userRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
        return Result.Success($"Delete employee {user.EmployeeCode} successfully");
    }
}

