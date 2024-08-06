
using Benchmark.Services;
using BenchmarkDotNet.Attributes;

namespace Benchmark;
[HtmlExporter]
public class Benchmark
{
    [Params(1, 5)]
    public int Iteration;
    private readonly Client _client = new Client();
    private readonly ClientGood _clientGood = new ClientGood();


    [Benchmark]
    public async Task Get()
    {
        for (int i = 0;i< Iteration; i++)
        {
            await _client.Get();
        }
    }
    [Benchmark]
    public async Task GetAsNoTracking()
    {
        for (int i = 0; i < Iteration; i++)
        {
            await _clientGood.GetAsNoTracking();
        }
    }
}
