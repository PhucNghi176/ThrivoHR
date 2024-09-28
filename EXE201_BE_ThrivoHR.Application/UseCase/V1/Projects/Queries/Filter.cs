using EXE201_BE_ThrivoHR.Domain.Entities;
using TaskStatus = EXE201_BE_ThrivoHR.Domain.Common.Status.TaskStatus;
namespace EXE201_BE_ThrivoHR.Application.UseCase.V1.Projects.Queries;

public record Filter(string? ProjectId, string? Name, string? LeaderName, string? SubLeaderName, TaskStatus? Status, int PageSize=100, int PageNo=1) : IQuery<PagedResult<ProjectDto>>;
internal sealed class FilterHandler(IProjectRepository projectRepository, IMapper mapper) : IQueryHandler<Filter, PagedResult<ProjectDto>>
{
    public async Task<Result<PagedResult<ProjectDto>>> Handle(Filter request, CancellationToken cancellationToken)
    {
        IQueryable<Project> filter(IQueryable<Project> x)
        {
            x = x.Where(x =>
            (string.IsNullOrEmpty(request.ProjectId) || x.Id == request.ProjectId) &&
            (string.IsNullOrEmpty(request.Name) || x.Name.Contains(request.Name)) &&
            (string.IsNullOrEmpty(request.LeaderName) || x.Leader.FullName.Contains(request.LeaderName)) &&
            (string.IsNullOrEmpty(request.SubLeaderName) || x.SubLeader.FullName.Contains(request.SubLeaderName)) &&
            (string.IsNullOrEmpty(request.Status.ToString()) || x.Status.ToString() == request.Status.ToString()));



            return x;
        }
        var result = await projectRepository.FindAllAsync(request.PageNo, request.PageSize, filter, cancellationToken);
        return Result.Success(PagedResult<ProjectDto>.Create(result.TotalCount, result.PageCount, result.PageSize, result.PageNo, result.ToListProjectDtos(mapper)));
    }
}
