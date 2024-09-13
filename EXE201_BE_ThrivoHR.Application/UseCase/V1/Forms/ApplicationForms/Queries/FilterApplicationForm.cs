
using EXE201_BE_ThrivoHR.Domain.Entities.Forms;

namespace EXE201_BE_ThrivoHR.Application.UseCase.V1.Forms.ApplicationForms.Queries;

public record FilterApplicationForm(
    string? FullName,
    int PageNumber = 1,
    int PageSize = 100) : IQuery<PagedResult<ApplicationFormDto>>;
internal sealed class FilterApplicationFormHanlder : IQueryHandler<FilterApplicationForm, PagedResult<ApplicationFormDto>>
{
    private readonly IApplicationFormRepository _applicationFormRepository;
    private readonly IMapper _mapper;

    public FilterApplicationFormHanlder(IApplicationFormRepository applicationFormRepository, IMapper mapper)
    {
        _applicationFormRepository = applicationFormRepository;
        _mapper = mapper;
    }

    public async Task<Result<PagedResult<ApplicationFormDto>>> Handle(FilterApplicationForm request, CancellationToken cancellationToken)
    {
        IQueryable<ApplicationForm> query(IQueryable<ApplicationForm> x)
        {

            x = x.Where(x => string.IsNullOrEmpty(request.FullName) || x.FullName.Contains(request.FullName));
            return x;
        }
        var list = await _applicationFormRepository.FindAllAsync(pageNo: request.PageNumber, pageSize: request.PageSize, query, cancellationToken);
        return Result.Success(PagedResult<ApplicationFormDto>.Create(list.TotalCount, list.PageCount, list.PageSize, list.PageNo, list.MapToApplicationFormListDto(_mapper)));
    }
}

