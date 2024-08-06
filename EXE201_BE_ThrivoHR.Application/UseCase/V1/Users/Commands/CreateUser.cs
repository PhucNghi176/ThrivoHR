using EXE201_BE_ThrivoHR.Application.Common.Exceptions;
using EXE201_BE_ThrivoHR.Application.Model;
using EXE201_BE_ThrivoHR.Domain.Entities;
using EXE201_BE_ThrivoHR.Domain.Entities.Identity;
using static EXE201_BE_ThrivoHR.Application.Common.Exceptions.Employee;

namespace EXE201_BE_ThrivoHR.Application.UseCase.V1.Users.Commands;

public record CreateUser(EmployeeModel Employee) : ICommand<string>;

internal sealed class CreateUserHandler : ICommandHandler<CreateUser, string>
{
    private readonly IAddressRepository _addressRepository;
    private readonly IUserRepository _userRepository;
    private readonly ICurrentUserService _currentUserService;
    private readonly IMapper _mapper;

    public CreateUserHandler(IAddressRepository addressRepository, IUserRepository userRepository, IMapper mapper, ICurrentUserService currentUserService)
    {
        _addressRepository = addressRepository;
        _userRepository = userRepository;
        _mapper = mapper;
        _currentUserService = currentUserService;
    }

    public async Task<Result<string>> Handle(CreateUser request, CancellationToken cancellationToken)
    {


        var address = _mapper.Map<Address>(request.Employee.Address);
        await _addressRepository.Add(address);
        var employee = _mapper.Map<AppUser>(request.Employee);
        employee.CreatedBy = _currentUserService.UserId;
        await _userRepository.Add(employee);
        int success = await _userRepository.UnitOfWork.SaveChangesAsync(cancellationToken);

        if (success == 0)
        {
            throw new CreateFailureException(nameof(AppUser), request.Employee);
        }
        return Result.Success(employee.EmployeeCode);


    }
}

