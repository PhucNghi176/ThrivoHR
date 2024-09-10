
namespace EXE201_BE_ThrivoHR.Application.UseCase.V1.Departments.Queries;

public record Get : IQuery<Dictionary<int, string?>>;
internal sealed class GetQueryHander : IQueryHandler<Get, Dictionary<int, string?>>
{
    private readonly IDepartmentRepository _departmentRepository;

    public GetQueryHander(IDepartmentRepository departmentRepository)
    {
        _departmentRepository = departmentRepository;
    }

    public async Task<Result<Dictionary<int, string?>>> Handle(Get request, CancellationToken cancellationToken)
    {
        var list = await _departmentRepository.FindAllAsync(cancellationToken);
        var result = list.ToDictionary(x => x.Id, x => x.Name);
        return Result.Success(result);
    }
}
