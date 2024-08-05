
using Ardalis.Specification;
using EXE201_BE_ThrivoHR.Application.Common.Specification;
using EXE201_BE_ThrivoHR.Domain.Entities.Identity;
using System;

namespace EXE201_BE_ThrivoHR.Application.UseCase.V1.Users.Queries;

public record Test(PaginationFilter filter) : IQuery<PagedResult<EmployeeDto>>;

internal sealed class TestHandler : IQueryHandler<Test, PagedResult<EmployeeDto>>
{
    private readonly IUserRepository _userRepository;
    private readonly IReadRepositoryBase<AppUser> _readRepository;
    private readonly IMapper _mapper;

    public TestHandler(IUserRepository userRepository, IMapper mapper, IReadRepositoryBase<AppUser> readRepository)
    {
        _userRepository = userRepository;
        _mapper = mapper;
        _readRepository = readRepository;
    }

    public async Task<Result<PagedResult<EmployeeDto>>> Handle(Test request, CancellationToken cancellationToken)
    {
        var spec = new EntitiesByBaseFilterSpec<AppUser, EmployeeDto>(request.filter);
        var users = await _readRepository.ListAsync(spec, cancellationToken).ConfigureAwait(false);
        var total = await _readRepository.CountAsync(spec, cancellationToken).ConfigureAwait(false);

        return Result.Success(PagedResult<EmployeeDto>.Create(total, total, request.filter.PageSize, request.filter.PageNumber, users));
    }
}

