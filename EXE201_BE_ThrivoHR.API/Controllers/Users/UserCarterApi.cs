using Carter;
using EXE201_BE_ThrivoHR.Application.UseCase.V1.Users.Commands;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EXE201_BE_ThrivoHR.API.Controllers.Users;


public class UserCarterApi : ICarterModule
{
    private const string BaseUrl = "/api/v{version:apiVersion}/users";
    private readonly ISender _sender;

    public UserCarterApi(ISender sender)
    {
        _sender = sender;
    }

    public void AddRoutes(IEndpointRouteBuilder app)
    {

        var group1 = app.NewVersionedApi("users")
             .MapGroup(BaseUrl).HasApiVersion(1);

        group1.MapPost("generate", GenerateUsers);

    }

    public static async Task<IResult> GenerateUsers(ISender sender,  GenerateEmployeesCommand generateEmployeesCommand)
    {
        var result = await sender.Send(generateEmployeesCommand);
        return result.IsSuccess ? (IResult)Results.Ok() : Results.BadRequest();
    }
}
