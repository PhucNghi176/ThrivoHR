using EXE201_BE_ThrivoHR.Application.Common.Exceptions;
using EXE201_BE_ThrivoHR.Domain.Common.Exceptions;
using FluentValidation;

namespace EXE201_BE_ThrivoHR.Application.UseCase.Authentication;

public record LoginQuery(string EmployeeCode, string Password) : IQuery<string>;

internal sealed class LoginQueryHandler : IQueryHandler<LoginQuery, string>
{
    private readonly IUserRepository _userRepository;


    public LoginQueryHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;

    }

    public async Task<Result<string>> Handle(LoginQuery request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.FindAsync(x => x.EmploeeyCode == request.EmployeeCode && !x.LockoutEnabled, cancellationToken) ?? throw new Employee.NotFoundException(request.EmployeeCode);
        var PasswordMatched = _userRepository.VerifyPassword(request.Password, user.PasswordHash);
        if (!PasswordMatched)
        {
            Error error = new Error("401", "Invalid Password");
            return (Result<string>)Result.Failure(error);
        }
        return Result.Success("Login Successful");
    }
}
public class LoginQueryValidator : AbstractValidator<LoginQuery>
{
    public LoginQueryValidator()
    {
        RuleFor(x => x.EmployeeCode).NotEmpty().NotNull().WithMessage("Employee Code is required");
        RuleFor(x => x.Password).NotEmpty().NotNull().WithMessage("Password is required");
    }
}

