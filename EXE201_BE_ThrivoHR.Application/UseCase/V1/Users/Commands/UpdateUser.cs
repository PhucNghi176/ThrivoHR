using EXE201_BE_ThrivoHR.Application.Common.Method;
using EXE201_BE_ThrivoHR.Application.Model;
using EXE201_BE_ThrivoHR.Domain.Entities.Identity;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using static EXE201_BE_ThrivoHR.Application.Common.Exceptions.Employee;

namespace EXE201_BE_ThrivoHR.Application.UseCase.V1.Users.Commands;

public record UpdateUser(EmployeeModelRequest EmployeeModel) : ICommand;
internal sealed class UpdateUserHandler(IEmployeeRepository userRepository, IMapper mapper) : ICommandHandler<UpdateUser>
{
    private readonly IEmployeeRepository _userRepository = userRepository;
    private readonly IMapper _mapper = mapper;

    public async Task<Result> Handle(UpdateUser request, CancellationToken cancellationToken)
    {
        var employee = await _userRepository.FindAsync(x => x.EmployeeId == EmployeesMethod.ConvertEmployeeCodeToId(request.EmployeeModel.EmployeeCode!), cancellationToken) ?? throw new NotFoundException(request.EmployeeModel.EmployeeCode!);
        employee = _mapper.Map(request.EmployeeModel, employee);
        await _userRepository.UpdateAsync(employee);
        try
        {
            await _userRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
        }
        catch (DbUpdateException ex)//Handle dupplication exception
        {

            throw new DuplicateException(nameof(AppUser), ExceptionMethod.GetKeyString(ex.ToString()));
        }
        catch (Exception)
        {
            throw new CreateFailureException(nameof(AppUser));
        }

        return Result.Success();

    }

}
public sealed class UpdateUserValidator : AbstractValidator<UpdateUser>
{
    public UpdateUserValidator()
    {

        RuleFor(x => x.EmployeeModel.FirstName).Cascade(CascadeMode.Stop).NotEmpty().NotNull().WithMessage("First Name is required");
        RuleFor(x => x.EmployeeModel.LastName).Cascade(CascadeMode.Stop).NotEmpty().NotNull().WithMessage("Last Name is required");
        RuleFor(x => x.EmployeeModel.IdentityNumber).Cascade(CascadeMode.Stop).NotEmpty().NotNull().WithMessage("Identity Number is required");
        RuleFor(x => x.EmployeeModel.DateOfBirth).Cascade(CascadeMode.Stop).NotEmpty().NotNull().WithMessage("Date of Birth is required");
        RuleFor(x => x.EmployeeModel.PhoneNumber).Cascade(CascadeMode.Stop).NotEmpty().NotNull().WithMessage("Phone Number is required");

        RuleFor(x => x.EmployeeModel.BankAccount).Cascade(CascadeMode.Stop).NotEmpty().NotNull().WithMessage("Bank Account is required");
        RuleFor(x => x.EmployeeModel.Address).Cascade(CascadeMode.Stop).NotNull().WithMessage("Address is required");
        RuleFor(x => x.EmployeeModel.Address!.AddressLine).Cascade(CascadeMode.Stop).NotEmpty().NotNull().WithMessage("Address Line is required");

    }
}

