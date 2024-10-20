using EXE201_BE_ThrivoHR.Domain.Entities;

namespace EXE201_BE_ThrivoHR.Application.UseCase.FaceDetection;
public record Filter(int PageSize, int PageNumber) : ICommand<PagedResult<AttendanceDto>>;

internal sealed class FilterHandler(
    IAttendanceRepository attendanceRepository,
    IMapper mapper) : ICommandHandler<Filter, PagedResult<AttendanceDto>>
{
    public async Task<Result<PagedResult<AttendanceDto>>> Handle(Filter request, CancellationToken cancellationToken)
    {
        var result = await attendanceRepository.FindAllAsync(request.PageSize, request.PageNumber, cancellationToken);
        return PagedResult<AttendanceDto>.Create(result.TotalCount, result.PageCount, result.PageSize, result.PageNo,
            result.MapToAttendanceListDto(mapper));
    }
}