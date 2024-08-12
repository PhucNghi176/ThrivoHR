using EXE201_BE_ThrivoHR.Application.Common.Method;
using EXE201_BE_ThrivoHR.Application.Model;
using EXE201_BE_ThrivoHR.Domain.Entities.Identity;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using static EXE201_BE_ThrivoHR.Application.Common.Exceptions.Employee;

namespace EXE201_BE_ThrivoHR.Application.UseCase.V1.Users.Commands;

public record UpdateUser(EmployeeModel employeeModel) : ICommand;
internal sealed class UpdateUserHandler : ICommandHandler<UpdateUser>
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    private readonly ICurrentUserService _currentUserService;
    private readonly IAddressRepository _addressRepository;

    public UpdateUserHandler(IUserRepository userRepository, IMapper mapper, ICurrentUserService currentUserService, IAddressRepository addressRepository)
    {
        _userRepository = userRepository;
        _mapper = mapper;
        _currentUserService = currentUserService;
        _addressRepository = addressRepository;
    }

    public async Task<Result> Handle(UpdateUser request, CancellationToken cancellationToken)
    {
        var employee = await _userRepository.FindAsync(x => x.EmployeeId == EmployeesMethod.ConvertEmployeeCodeToId(request.employeeModel.EmployeeCode!), cancellationToken) ?? throw new NotFoundException(request.employeeModel.EmployeeCode!);
        var address = _mapper.Map(request.employeeModel.Address, employee.Address);
        address!.LastModifiedBy = _currentUserService.UserId;
        address.LastModifiedOn = DateTime.UtcNow.AddHours(7);
        await _addressRepository.UpdateAsync(address);
        employee = _mapper.Map(request.employeeModel, employee);
        employee.LastModifiedBy = _currentUserService.UserId;
        employee.LastModifiedOn = DateTime.UtcNow.AddHours(7);
        await _userRepository.UpdateAsync(employee);
        try
        {
            await _userRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
        }
        catch (DbUpdateException ex)//Handle dupplication exception
        {

            throw new DuplicateException(nameof(AppUser), ExceptionMethod.GetKeyString(ex.ToString()));
        }
        catch (Exception ex)
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
        RuleFor(x => x.employeeModel.EmployeeCode).Cascade(CascadeMode.Stop).NotEmpty().NotNull().WithMessage("Employee Code is required");
        RuleFor(x => x.employeeModel.FirstName).Cascade(CascadeMode.Stop).NotEmpty().NotNull().WithMessage("First Name is required");
        RuleFor(x => x.employeeModel.LastName).Cascade(CascadeMode.Stop).NotEmpty().NotNull().WithMessage("Last Name is required");
        RuleFor(x => x.employeeModel.IdentityNumber).Cascade(CascadeMode.Stop).NotEmpty().NotNull().WithMessage("Identity Number is required");
        RuleFor(x => x.employeeModel.DateOfBirth).Cascade(CascadeMode.Stop).NotEmpty().NotNull().WithMessage("Date of Birth is required");
        RuleFor(x => x.employeeModel.PhoneNumber).Cascade(CascadeMode.Stop).NotEmpty().NotNull().WithMessage("Phone Number is required");
        RuleFor(x => x.employeeModel.DepartmentId).Cascade(CascadeMode.Stop).NotEmpty().NotNull().WithMessage("Department is required");
        RuleFor(x => x.employeeModel.PositionId).Cascade(CascadeMode.Stop).NotEmpty().NotNull().WithMessage("Position is required");
        RuleFor(x => x.employeeModel.BankAccount).Cascade(CascadeMode.Stop).NotEmpty().NotNull().WithMessage("Bank Account is required");
        RuleFor(x => x.employeeModel.Address).Cascade(CascadeMode.Stop).NotNull().WithMessage("Address is required");
        RuleFor(x => x.employeeModel.Address!.AddressLine).Cascade(CascadeMode.Stop).NotEmpty().NotNull().WithMessage("Address Line is required");

    }
}

