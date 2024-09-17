using EXE201_BE_ThrivoHR.Domain.Entities;

namespace EXE201_BE_ThrivoHR.Application.UseCase.V1.Unions.Queries;

public record FilterUnion(string? EmployeeCode, string? Title, DateOnly? DateJoined, int PageSize = 100, int PageNumber = 1) : IQuery<PagedResult<UnionDto>>;
internal sealed class FilterUnionHandler : IQueryHandler<FilterUnion, PagedResult<UnionDto>>
{
    private readonly IUnionRepository _unionRepository;
    private readonly IMapper _mapper;

    public FilterUnionHandler(IUnionRepository unionRepository, IMapper mapper)
    {
        _unionRepository = unionRepository;
        _mapper = mapper;
    }

    public async Task<Result<PagedResult<UnionDto>>> Handle(FilterUnion request, CancellationToken cancellationToken)
    {
        IQueryable<Union> unions(IQueryable<Union> x)
        {
            x = x.Where(x =>
                (string.IsNullOrEmpty(request.EmployeeCode) || x.Employee!.EmployeeCode == request.EmployeeCode)
                && (string.IsNullOrEmpty(request.Title) || x.Title.Contains(request.Title))
                && (!request.DateJoined.HasValue || x.DateJoined == request.DateJoined)
            );

            return x;
        }
        var list = await _unionRepository.FindAllAsync(request.PageNumber, request.PageSize, unions, cancellationToken);
        var mapped = PagedResult<UnionDto>.Create(list.TotalCount, list.PageCount, list.PageSize, list.PageNo, list.MapToUnionListDto(_mapper));
        return Result.Success(mapped);
    }
}
