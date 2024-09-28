
namespace EXE201_BE_ThrivoHR.Application.UseCase.V1.Projects.Queries;

public record GetAllMember(string ProjectId) : IQuery<List<string>>;
internal sealed class GetAllMemberHandler(IEmployeesProjectMappingRepository employeesProjectMappingRepository) : IQueryHandler<GetAllMember, List<string>>
{
    public async Task<Result<List<string>>> Handle(GetAllMember request, CancellationToken cancellationToken)
    {
        var result = await employeesProjectMappingRepository.FindAllAsync(x => x.ProjectId == request.ProjectId, cancellationToken);
        return Result.Success(result.Select(x => x.Employee.FullName).ToList());
    }
}



