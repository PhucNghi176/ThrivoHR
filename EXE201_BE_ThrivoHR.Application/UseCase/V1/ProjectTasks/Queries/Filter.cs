

using EXE201_BE_ThrivoHR.Domain.Entities;
using TaskStatus = EXE201_BE_ThrivoHR.Domain.Common.Status.TaskStatus;
namespace EXE201_BE_ThrivoHR.Application.UseCase.V1.ProjectTasks.Queries;

public record Filter(
    string? ProjectId,
    string? TaskId,
    string? TaskName,
    string? TaskDescription,
    TaskStatus? TaskStatus,
    DateTime? TaskEndDate,
    string? TaskAssignee,
    int PageSize = 100,
    int PageNumber = 1


    ) : IQuery<PagedResult<ProjectTaskDto>>;
internal sealed class FilterHandler(IProjectTaskRepository projectTaskRepository, IMapper mapper) : IQueryHandler<Filter, PagedResult<ProjectTaskDto>>
{
    public async Task<Result<PagedResult<ProjectTaskDto>>> Handle(Filter request, CancellationToken cancellationToken)
    {
        IQueryable<ProjectTask> filter(IQueryable<ProjectTask> x)
        {
            x = x.Where(x =>
                (string.IsNullOrEmpty(request.ProjectId) || x.ProjectId == request.ProjectId)
                && (string.IsNullOrEmpty(request.TaskId) || x.Id == request.TaskId)
                && (string.IsNullOrEmpty(request.TaskName) || x.Name.Contains(request.TaskName))
                && (string.IsNullOrEmpty(request.TaskDescription) || x.Description.Contains(request.TaskDescription))
                && (!request.TaskStatus.HasValue || x.Status.ToString().Contains(request.TaskStatus.ToString()))
                && (!request.TaskEndDate.HasValue || x.DueDate == request.TaskEndDate)
                && (string.IsNullOrEmpty(request.TaskAssignee) || x.Assignee.FullName.Contains(request.TaskAssignee)));
            return x;
        }


        var r = await projectTaskRepository.FindAllAsync(request.PageNumber, request.PageSize, filter, cancellationToken);
        return Result.Success(PagedResult<ProjectTaskDto>.Create(r.TotalCount, r.PageCount, r.PageSize, r.PageNo, r.ToProjectTaskDtos(mapper)));



    }
}

