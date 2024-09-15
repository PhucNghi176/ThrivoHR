using EXE201_BE_ThrivoHR.Application.Common.Exceptions;
using EXE201_BE_ThrivoHR.Application.Common.Method;
using EXE201_BE_ThrivoHR.Application.Model;
using FluentValidation;

namespace EXE201_BE_ThrivoHR.Application.UseCase.Authentication;

public record LoginQuery(string EmployeeCode, string Password) : IQuery<TokenModel>;

internal sealed class LoginQueryHandler(IEmployeeRepository userRepository, ITokenService tokenService) : IQueryHandler<LoginQuery, TokenModel>
{
    public async Task<Result<TokenModel>> Handle(LoginQuery request, CancellationToken cancellationToken)
    {
        var user = await userRepository.FindAsync(x => x.EmployeeId == EmployeesMethod.ConvertEmployeeCodeToId(request.EmployeeCode) && !x.LockoutEnabled, cancellationToken) ?? throw new Employee.NotFoundException(request.EmployeeCode);
        var PasswordMatched = await userRepository.VerifyPasswordAsync(request.Password, user.PasswordHash!);
        if (!PasswordMatched)
        {
            throw new Employee.PasswordMismatchException();
        }
        var RoleName = user.UserRoles!.FirstOrDefault()!.RoleId.ToString() switch
        {
            "1" => "Admin",
            "2" => "HR",
            "3" => "C&B",
            _ => throw new Employee.RoleNotFoundException()
        };
        var token = await tokenService.GenerateTokenAsync(user.Id, RoleName);
        user.RefreshToken = token.RefreshToken;
        user.RefreshTokenExpiryTime = DateTime.Now.AddDays(7);
        await userRepository.UpdateAsync(user);
        await userRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
        return Result.Success(token);
    }

}
public sealed class LoginQueryValidator : AbstractValidator<LoginQuery>
{
    public LoginQueryValidator()
    {
        RuleFor(x => x.EmployeeCode).Cascade(CascadeMode.Stop).NotEmpty().NotNull().WithMessage("Employee Code is required");
        RuleFor(x => x.Password).Cascade(CascadeMode.Stop).NotEmpty().NotNull().WithMessage("Password is required");
    }
}

