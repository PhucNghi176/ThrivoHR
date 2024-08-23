using EXE201_BE_ThrivoHR.Application.Common.Method;
using EXE201_BE_ThrivoHR.Application.Model;
using EXE201_BE_ThrivoHR.Domain.Entities;
using EXE201_BE_ThrivoHR.Domain.Entities.Identity;
using EXE201_BE_ThrivoHR.Domain.Services;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using static EXE201_BE_ThrivoHR.Application.Common.Exceptions.Employee;

namespace EXE201_BE_ThrivoHR.Application.UseCase.V1.Users.Commands;

public record CreateUser(EmployeeModel Employee) : ICommand<string>;

internal sealed class CreateUserHandler(IEmployeeRepository userRepository, IMapper mapper) : ICommandHandler<CreateUser, string>
{
    public async Task<Result<string>> Handle(CreateUser request, CancellationToken cancellationToken)
    {


        var employee = mapper.Map<AppUser>(request.Employee);
        await userRepository.AddAsync(employee);
        try
        {
            await userRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
        }
        catch (DbUpdateException ex)//Handle dupplication exception
        {

            throw new DuplicateException(nameof(AppUser), ExceptionMethod.GetKeyString(ex.ToString()));
        }
        catch (Exception)
        {
            throw new CreateFailureException(nameof(AppUser));
        }



        return Result.Success(employee.EmployeeCode);


    }
}
public sealed class CreateUserValidator : AbstractValidator<CreateUser>
{
    public CreateUserValidator()
    {
        RuleFor(x => x.Employee.EmployeeCode).Cascade(CascadeMode.Stop).NotEmpty().NotNull().WithMessage("Employee Code is required");
        RuleFor(x => x.Employee.FirstName).Cascade(CascadeMode.Stop).NotEmpty().NotNull().WithMessage("First Name is required");
        RuleFor(x => x.Employee.LastName).Cascade(CascadeMode.Stop).NotEmpty().NotNull().WithMessage("Last Name is required");
        RuleFor(x => x.Employee.IdentityNumber).Cascade(CascadeMode.Stop).NotEmpty().NotNull().WithMessage("Identity Number is required");
        RuleFor(x => x.Employee.DateOfBirth).Cascade(CascadeMode.Stop).NotEmpty().NotNull().WithMessage("Date of Birth is required");
        RuleFor(x => x.Employee.PhoneNumber).Cascade(CascadeMode.Stop).NotEmpty().NotNull().WithMessage("Phone Number is required");
        RuleFor(x => x.Employee.BankAccount).Cascade(CascadeMode.Stop).NotEmpty().NotNull().WithMessage("Bank Account is required");
        RuleFor(x => x.Employee.Address).Cascade(CascadeMode.Stop).NotNull().WithMessage("Address is required");
        RuleFor(x => x.Employee.Address!.AddressLine).Cascade(CascadeMode.Stop).NotEmpty().NotNull().WithMessage("Address Line is required");

    }
}


