
using EXE201_BE_ThrivoHR.Application.Common.Method;
using EXE201_BE_ThrivoHR.Application.Common.Security;
using EXE201_BE_ThrivoHR.Domain.Services;
using static EXE201_BE_ThrivoHR.Application.Common.Exceptions.Employee;

namespace EXE201_BE_ThrivoHR.Application.UseCase.V1.Users.Commands;

[Authorize]
public record DeleteUser(string EmployeeCode) : ICommand<string>;

internal sealed class DeleteUserCommandHandler : ICommandHandler<DeleteUser, string>
{
    private readonly IUserRepository _userRepository;
    private readonly ICurrentUserService _currentUserService;

    public DeleteUserCommandHandler(IUserRepository userRepository, ICurrentUserService currentUserService)
    {
        _userRepository = userRepository;
        _currentUserService = currentUserService;
    }

    public async Task<Result<string>> Handle(DeleteUser request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.FindAsync(x => x.EmployeeId == EmployeesMethod.ConvertEmployeeCodeToId(request.EmployeeCode), cancellationToken) ?? throw new NotFoundException(request.EmployeeCode);
        user.LockoutEnabled = true;
        user.DeletedOn = DateTime.UtcNow.AddHours(7);
        user.DeletedBy = _currentUserService.UserId;
        await _userRepository.UpdateAsync(user);
        await _userRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
        return Result.Success($"Delete employee {user.EmployeeCode} successfully");


    }
}

