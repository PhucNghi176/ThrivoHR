using EXE201_BE_ThrivoHR.API.Services;
using EXE201_BE_ThrivoHR.Application.Common.Models;
using EXE201_BE_ThrivoHR.Application.Common.Pagination;
using EXE201_BE_ThrivoHR.Application.Model;
using EXE201_BE_ThrivoHR.Application.UseCase.V1.Users;
using EXE201_BE_ThrivoHR.Domain.Entities.Identity;
using EXE201_BE_ThrivoHR.Test.Base;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using Xunit;

namespace EXE201_BE_ThrivoHR.Test;

public class UserControllerTest : IClassFixture<TestFixture>
{
    private readonly BaseTestController _baseTestController;
    private readonly HttpClient _client;
    private readonly CancellationToken token;
    public UserControllerTest(TestFixture fixture)
    {
        _baseTestController = fixture.BaseTestController;
        var factory = new ApiWebApplicationFactory();
        _client = factory.CreateClient();
        token = CancellationToken.None;
    }

    [Fact]
    public async Task GetEmployees_Success()
    {

        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await _baseTestController.CreateToken());
        var response = await _client.GetAsync("/api/v1/employee?PageNumber=1&PageSize=1");
        response.EnsureSuccessStatusCode();
        Assert.NotNull(response);//make sure the response is not null
        Assert.True(response.IsSuccessStatusCode);//make sure the response is success
        //make sure the response is not null
    }
    [Fact]
    public async Task CreateEmployee_Success()
    {
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await _baseTestController.CreateToken());
        var employee = new EmployeeModel

        {
            FirstName = "nghi1",
            LastName = "string14",
            FullName = "string14",
            IdentityNumber = "string11423",
            DateOfBirth = DateOnly.Parse("2024/06/05"),
            PhoneNumber = "string41",
            TaxCode = "string41",
            DepartmentId = 1,
            PositionId = 1,
            BankAccount = "string112354",
            Email = "string41",
            Address = new AddressModel
            {
                City = "string",
                District = "string",
                AddressLine = "string",
                Ward = "string",
                Country = "string"

            }
        };
        var res = await _client.PostAsJsonAsync("/api/v1/employee", new 
        {


             employee
        }, cancellationToken: token);
        
        
        var re= await res.Content.ReadAsStringAsync();
        Assert.NotNull(res);
        Assert.True(res.IsSuccessStatusCode);


    }
}