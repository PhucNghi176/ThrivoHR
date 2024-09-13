using EXE201_BE_ThrivoHR.Domain.Entities.Forms;

namespace EXE201_BE_ThrivoHR.Application.UseCase.V1.Forms.ResignForms.Queries;

public record FilterResignForms(string? FullName,int PageNumber = 1,
    int PageSize = 100) : IQuery<PagedResult<ResginFormDto>>;
internal sealed class FilterResignFormsHandler : IQueryHandler<FilterResignForms, PagedResult<ResginFormDto>>
{
    private readonly IResignFormRepository _resignFormRepository;
    private readonly IMapper _mapper;

    public FilterResignFormsHandler(IResignFormRepository resignFormRepository, IMapper mapper)
    {
        _resignFormRepository = resignFormRepository;
        _mapper = mapper;
    }

    public async Task<Result<PagedResult<ResginFormDto>>> Handle(FilterResignForms request, CancellationToken cancellationToken)
    {
        IQueryable<ResignForm> query(IQueryable<ResignForm> x)
        {

            x = x.Where(x => string.IsNullOrEmpty(request.FullName) || x.Employee.FullName.Contains(request.FullName));
            return x;
        }
        var list = await _resignFormRepository.FindAllAsync(pageNo: request.PageNumber, pageSize: request.PageSize, query, cancellationToken);
        return Result.Success(PagedResult<ResginFormDto>.Create(list.TotalCount, list.PageCount, list.PageSize, list.PageNo, list.MapToResignFormListDto(_mapper)));
    }
}
