using EXE201_BE_ThrivoHR.Application.Common.Method;
using EXE201_BE_ThrivoHR.Application.Model;
using EXE201_BE_ThrivoHR.Domain.Entities;
using EXE201_BE_ThrivoHR.Domain.Entities.Identity;
using EXE201_BE_ThrivoHR.Domain.Services;
using Microsoft.EntityFrameworkCore;
using static EXE201_BE_ThrivoHR.Application.Common.Exceptions.Employee;

namespace EXE201_BE_ThrivoHR.Application.UseCase.V1.Users.Commands;

public record CreateUser(EmployeeModel Employee) : ICommand<string>;

internal sealed class CreateUserHandler(IUserRepository userRepository, IMapper mapper) : ICommandHandler<CreateUser, string>
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

