
using EXE201_BE_ThrivoHR.Application.Model;
using Microsoft.AspNetCore.JsonPatch;

namespace EXE201_BE_ThrivoHR.Application.UseCase.V1.Users.Commands;



public record UpdateUser(JsonPatchDocument<EmployeeModel> Employee) : ICommand<string>;

internal sealed class UpdateUserCommandHandler : ICommandHandler<UpdateUser, string>
{
    public Task<Result<string>> Handle(UpdateUser request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
