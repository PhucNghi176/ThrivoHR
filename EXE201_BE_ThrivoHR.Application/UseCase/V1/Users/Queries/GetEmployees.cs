using EXE201_BE_ThrivoHR.Domain.Entities.Identity;
using MediatR;

namespace EXE201_BE_ThrivoHR.Application.UseCase.V1.Users.Queries;

public record GetEmployees(int PageNumber, int PageSize) : IRequest<List<AppUser>>;

internal sealed class GetEmployeesQueryHandler : IRequestHandler<GetEmployees, List<AppUser>>
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public GetEmployeesQueryHandler(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<List<AppUser>> Handle(GetEmployees request, CancellationToken cancellationToken)
    {
        var employees = await _userRepository.FindAllAsync(request.PageNumber, request.PageSize, cancellationToken);
        return employees.ToList();
    }
}
