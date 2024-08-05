using Carter;
using EXE201_BE_ThrivoHR.Application.Common.Models;
using EXE201_BE_ThrivoHR.Application.Common.Pagination;
using EXE201_BE_ThrivoHR.Application.UseCase.V1.Users.Commands;
using EXE201_BE_ThrivoHR.Application.UseCase.V1.Users.Queries;
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

        group1.MapGet("generate", GenerateUsers);
        group1.MapGet(string.Empty, Test);

    }

    public static async Task<IResult> GenerateUsers(ISender sender)
    {
        var result = await sender.Send(new GenerateEmployeesCommand());
        return result.IsSuccess ? (IResult)Results.Ok() : Results.BadRequest();
    }
    public static async Task<IResult> Test(ISender sender, PaginationFilter filter)
    {
        var result = await sender.Send(new Test(filter));
        return Results.Ok(result);
    }
}
