
namespace EXE201_BE_ThrivoHR.Application.UseCase.V1.Positions.Queries;

public record Get : IQuery<Dictionary<int, string?>>;
internal sealed class GetQueryHandler : IQueryHandler<Get, Dictionary<int, string?>>
{
    private readonly IPositionRepository _positionRepository;

    public GetQueryHandler(IPositionRepository positionRepository)
    {
        _positionRepository = positionRepository;
    }

    public async Task<Result<Dictionary<int, string?>>> Handle(Get request, CancellationToken cancellationToken)
    {
        var list = await _positionRepository.FindAllAsync(cancellationToken);
        var result = list.ToDictionary(x => x.Id, x => x.Name);
        return Result.Success(result);
    }
}
