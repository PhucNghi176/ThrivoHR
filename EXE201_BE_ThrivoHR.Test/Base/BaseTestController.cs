using EXE201_BE_ThrivoHR.API.Services;
using Newtonsoft.Json.Linq;
using System.Net.Http.Json;
using System.Text.Json;

namespace EXE201_BE_ThrivoHR.Test.Base;

public class BaseTestController
{

    public BaseTestController()
    {
    }
    public async Task<string> CreateToken()
    {
        await using var factory = new ApiWebApplicationFactory();
        using var client = factory.CreateClient();
        var response = await client.PostAsJsonAsync("/api/v1/authentication",
            new { employeeCode = "000001", password = "123456" });
        response.EnsureSuccessStatusCode();
        
        var jsonString = await response.Content.ReadAsStringAsync();
        // Parse the JSON string into a JObject
        var jsonObject = JObject.Parse(jsonString);

        // Extract the token from the JObject
        var token = jsonObject["value"]["token"].ToString();

        return token;
    }

}
