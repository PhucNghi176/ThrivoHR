
using EXE201_BE_ThrivoHR.Domain.Entities.Identity;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace Benchmark.Services;

public class Client
{
    private static readonly HttpClient _client = new HttpClient();
    private const string BaseUrl = "https://localhost:7274/api/v1/Employee";

    public Client()
    {
        _client.DefaultRequestHeaders.Accept.Clear();
        _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
    }

    public async Task<List<AppUser>> Get()
    {
        return await _client.GetFromJsonAsync<List<AppUser>>($"{BaseUrl}/GetEmployee?PageNumber=1&PageSize=100");
    }

}
