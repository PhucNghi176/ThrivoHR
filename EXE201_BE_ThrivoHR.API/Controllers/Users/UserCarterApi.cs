using EXE201_BE_ThrivoHR.Application.UseCase.V1.Users.Commands;
using EXE201_BE_ThrivoHR.Application.UseCase.V1.Users.Queries;

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
        group1.MapGet(string.Empty, GetUsers);

    }

    private static async Task<IResult> GenerateUsers(ISender sender)
    {
        var result = await sender.Send(new GenerateEmployeesCommand());
        return result.IsSuccess ? Results.Ok() : Results.BadRequest();
    }
    private static async Task<IResult> GetUsers(ISender sender, int PageNumber, int PageSize)
    {
        var result = await sender.Send(new GetEmployees(PageNumber, PageSize));
        return result.IsSuccess ? Results.Ok(result.Value) : Results.BadRequest();
    }

}
