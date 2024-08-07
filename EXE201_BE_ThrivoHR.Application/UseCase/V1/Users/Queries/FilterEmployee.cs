using EXE201_BE_ThrivoHR.Domain.Entities.Identity;
using EXE201_BE_ThrivoHR.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using EXE201_BE_ThrivoHR.Application.Common.Method;
using EXE201_BE_ThrivoHR.Application.Common.Security;
using Microsoft.AspNetCore.Mvc;
namespace EXE201_BE_ThrivoHR.Application.UseCase.V1.Users.Queries;


[Authorize(Roles = "Admin")]

public record FilterEmployee
    (
        string? Email,
        string? EmployeeCode,
        string? FirstName,
        string? LastName,
        string? FullName,
        string? PhoneNumber,
        string? IdentityNumber,
        string? TaxCode,
        string? BankAccount,
        string? Address,
        DateOnly? DateOfBirth,

        int? DepartmentId = 0,
        int? PositionId = 0,
        int PageNumber = 1,
        int PageSize = 100
    ) : IQuery<PagedResult<EmployeeDto>>;


internal sealed class FilterEmployeeHandler : IQueryHandler<FilterEmployee, PagedResult<EmployeeDto>>
{
    private readonly ApplicationDbContext _context;
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    public FilterEmployeeHandler(ApplicationDbContext context, IUserRepository userRepository, IMapper mapper)
    {
        _context = context;
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<Result<PagedResult<EmployeeDto>>> Handle(FilterEmployee request, CancellationToken cancellationToken)
    {
        IQueryable<AppUser> filter(IQueryable<AppUser> x)
        {
            // 1. Combine multiple Where clauses
            x = x.Where(x => !x.LockoutEnabled
                && (string.IsNullOrEmpty(request.EmployeeCode) || x.EmployeeId == EmployeesMethod.ConvertEmployeeCodeToId(request.EmployeeCode))
                && (string.IsNullOrEmpty(request.Email) || x.Email!.Contains(request.Email))
                && (string.IsNullOrEmpty(request.FirstName) || x.FirstName.Contains(request.FirstName))
                && (string.IsNullOrEmpty(request.LastName) || x.LastName.Contains(request.LastName))
                && (string.IsNullOrEmpty(request.FullName) || x.FullName.Contains(request.FullName))
                && (string.IsNullOrEmpty(request.PhoneNumber) || x.PhoneNumber.Contains(request.PhoneNumber))
                && (string.IsNullOrEmpty(request.IdentityNumber) || x.IdentityNumber.Contains(request.IdentityNumber))
                && (string.IsNullOrEmpty(request.TaxCode) || x.TaxCode.Contains(request.TaxCode))
                && (string.IsNullOrEmpty(request.BankAccount) || x.BankAccount.Contains(request.BankAccount))
                && (!request.DateOfBirth.HasValue || x.DateOfBirth == request.DateOfBirth)
                && (request.DepartmentId == 0 || x.DepartmentId == request.DepartmentId)
                && (request.PositionId == 0 || x.PositionId == request.PositionId));

            // 2. Handle Address filtering separately
            if (!string.IsNullOrEmpty(request.Address))
            {
                x = x.Join(_context.Addresses,
                    employee => employee.AddressId,
                    address => address.Id,
                    (employee, address) => new { employee, address })
                .Where(x => EF.Functions.Like(x.address.AddressLine + ", " + x.address.Ward + ", " +
                            x.address.District + ", " + x.address.City + ", " +
                            x.address.Country, $"%{request.Address}%"))
                .Select(x => x.employee);
            }

            // 3. Apply ordering last
            x = x.OrderBy(x => x.EmployeeId);

            return x;
        }
        var list = await _userRepository.FindAllAsync(request.PageNumber, request.PageSize, filter, cancellationToken);
        return PagedResult<EmployeeDto>.Create(list.TotalCount, list.PageCount, list.PageSize, list.PageNo, list.MapToEmployeeListDto(_mapper));
    }
}

