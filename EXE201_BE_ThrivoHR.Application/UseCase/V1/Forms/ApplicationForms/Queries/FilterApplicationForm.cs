
namespace EXE201_BE_ThrivoHR.Application.UseCase.V1.Forms.ApplicationForms.Queries;

public record FilterApplicationForm :IQuery<PagedResult<ApplicationFormDto>>;
internal sealed class FilterApplicationFormHanlder : IQueryHandler<FilterApplicationForm, PagedResult<ApplicationFormDto>>
{
    private readonly IApplicationFormRepository _applicationFormRepository;
    private readonly IMapper _mapper;

    public FilterApplicationFormHanlder(IApplicationFormRepository applicationFormRepository, IMapper mapper)
    {
        _applicationFormRepository = applicationFormRepository;
        _mapper = mapper;
    }

    public Task<Result<PagedResult<ApplicationFormDto>>> Handle(FilterApplicationForm request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
