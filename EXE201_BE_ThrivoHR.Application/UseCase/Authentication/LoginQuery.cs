
using EXE201_BE_ThrivoHR.Application.Common.Exceptions;
using EXE201_BE_ThrivoHR.Application.Common.Method;
using EXE201_BE_ThrivoHR.Application.Model;
using EXE201_BE_ThrivoHR.Application.UseCase.V1.Users;
using FluentValidation;
using Microsoft.AspNetCore.Identity;

namespace EXE201_BE_ThrivoHR.Application.UseCase.Authentication;

public record LoginQuery(string EmployeeCode, string Password) : IQuery<TokenModel>;

internal sealed class LoginQueryHandler : IQueryHandler<LoginQuery, TokenModel>
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    private readonly ITokenService _tokenService;

    public LoginQueryHandler(IUserRepository userRepository, IMapper mapper, ITokenService tokenService)
    {
        _userRepository = userRepository;
        _mapper = mapper;
        _tokenService = tokenService;
    }

    public async Task<Result<TokenModel>> Handle(LoginQuery request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.FindAsync(x => x.EmployeeId == EmployeesMethod.ConvertEmployeeCodeToId(request.EmployeeCode), cancellationToken) ?? throw new Employee.NotFoundException(request.EmployeeCode);
        var PasswordMatched = await _userRepository.VerifyPasswordAsync(request.Password, user.PasswordHash!);
        if (!PasswordMatched)
        {
            throw new Employee.PasswordMismatchException();
        }
        var RoleName = user.UserRoles.FirstOrDefault()!.RoleId.ToString() switch
        {
            "1" => "Admin",
            "2" => "HR",
            "3" => "C&B",
            _ => throw new Employee.RoleNotFoundException()
        };
        var token = await _tokenService.GenerateTokenAsync(user.EmployeeCode, RoleName!);
        user.RefreshToken = token.RefreshToken;
        user.RefreshTokenExpiryTime = DateTime.Now.AddDays(7);
        await _userRepository.UpdateAsync(user);
        await _userRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
        return Result<string>.Success(token);
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

