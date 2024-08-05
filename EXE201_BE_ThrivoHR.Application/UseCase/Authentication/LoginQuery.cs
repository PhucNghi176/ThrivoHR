using EXE201_BE_ThrivoHR.Application.Common.Exceptions;
using EXE201_BE_ThrivoHR.Application.UseCase.V1.Users;
using FluentValidation;

namespace EXE201_BE_ThrivoHR.Application.UseCase.Authentication;

public record LoginQuery(string EmployeeCode, string Password) : IQuery<EmployeeDto>;

internal sealed class LoginQueryHandler : IQueryHandler<LoginQuery, EmployeeDto>
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    private readonly IPositionRepository _positionRepository;
    private readonly IDepartmentRepository _departmentRepository;

    public LoginQueryHandler(IUserRepository userRepository, IMapper mapper, IPositionRepository positionRepository, IDepartmentRepository departmentRepository)
    {
        _userRepository = userRepository;
        _mapper = mapper;
        _positionRepository = positionRepository;
        _departmentRepository = departmentRepository;
    }

    public async Task<Result<EmployeeDto>> Handle(LoginQuery request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.FindAsync(x => x.EmploeeyCode == request.EmployeeCode && !x.LockoutEnabled, cancellationToken) ?? throw new Employee.NotFoundException(request.EmployeeCode);
        var PasswordMatched = _userRepository.VerifyPassword(request.Password, user.PasswordHash);
        if (!PasswordMatched)
        {
            throw new Employee.PasswordMismatchException();
        }
        return Result.Success(_mapper.Map<EmployeeDto>(user));

    }

}


public class LoginQueryValidator : AbstractValidator<LoginQuery>
{
    public LoginQueryValidator()
    {
        RuleFor(x => x.EmployeeCode).Cascade(CascadeMode.Stop).NotEmpty().NotNull().WithMessage("Employee Code is required");
        RuleFor(x => x.Password).Cascade(CascadeMode.Stop).NotEmpty().NotNull().WithMessage("Password is required");
    }
}

