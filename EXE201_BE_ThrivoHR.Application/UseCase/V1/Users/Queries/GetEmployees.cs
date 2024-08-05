namespace EXE201_BE_ThrivoHR.Application.UseCase.V1.Users.Queries;

public record GetEmployees(int PageNumber, int PageSize) : IQuery<PagedResult<EmployeeDto>>;

internal sealed class GetEmployeesQueryHandler : IQueryHandler<GetEmployees, PagedResult<EmployeeDto>>
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public GetEmployeesQueryHandler(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<Result<PagedResult<EmployeeDto>>> Handle(GetEmployees request, CancellationToken cancellationToken)
    {
        var employees = await _userRepository.FindAllAsync(request.PageNumber, request.PageSize, cancellationToken);
        return PagedResult<EmployeeDto>.Create(employees.TotalCount,employees.PageCount,employees.PageSize,employees.PageNo,employees.MapTopEmployeeListDto(_mapper));
    }
}
