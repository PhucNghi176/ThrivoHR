using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using EXE201_BE_ThrivoHR.Domain.Entities.Identity;

namespace EXE201_BE_ThrivoHR.Application.UseCase.V1.Users.Queries;

public record GetEmployeesQuery(string? SearchTerm, string? SortColoumn, SortOrder? SortOrder, IDictionary<string, SortOrder>? SortColumnAndOrder, int PageNumber, int PageSize) : IQuery<PagedResult<EmployeeDto>>;

internal sealed class GetEmployeeQueryHandler : IQueryHandler<GetEmployeesQuery, PagedResult<EmployeeDto>>
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    public Task<Result<PagedResult<EmployeeDto>>> Handle(GetEmployeesQuery request, CancellationToken cancellationToken)
    {
        if (request.SortColumnAndOrder.Any()) // =>>  Raw Query when order by multi column
        {       
            // ============================================
            var productsQuery = string.IsNullOrWhiteSpace(request.SearchTerm)
                ? @$"SELECT * FROM {nameof(AppUser)} ORDER BY "
                : @$"SELECT * FROM {nameof(AppUser)}
                        WHERE {nameof(AppUser.FullName)} LIKE '%{request.SearchTerm}%'
                        OR {nameof(AppUser.EmploeeyCode)} LIKE '%{request.SearchTerm}%'
                        ORDER BY ";

            foreach (var item in request.SortColumnAndOrder)
                productsQuery += item.Value == SortOrder.Descending
                    ? $"{item.Key} DESC, "
                    : $"{item.Key} ASC, ";

            productsQuery = productsQuery.Remove(productsQuery.Length - 2);

            productsQuery += $" OFFSET {(PageIndex - 1) * PageSize} ROWS FETCH NEXT {PageSize} ROWS ONLY";

            var products = await _context.Products.FromSqlRaw(productsQuery)
                .ToListAsync(cancellationToken: cancellationToken);

            var totalCount = await _context.Products.CountAsync(cancellationToken);

            var productPagedResult = PagedResult<Domain.Entities.Product>.Create(products,
                PageIndex,
                PageSize,
                totalCount);

            var result = _mapper.Map<PagedResult<Response.ProductResponse>>(productPagedResult);

            return Result.Success(result);
        }
        else // =>> Entity Framework
        {
            var productsQuery = string.IsNullOrWhiteSpace(request.SearchTerm)
            ? _productRepository.FindAll()
            : _productRepository.FindAll(x => x.Name.Contains(request.SearchTerm) || x.Description.Contains(request.SearchTerm));

            productsQuery = request.SortOrder == SortOrder.Descending
            ? productsQuery.OrderByDescending(GetSortProperty(request))
            : productsQuery.OrderBy(GetSortProperty(request));

            var products = await PagedResult<Domain.Entities.Product>.CreateAsync(productsQuery,
                request.PageIndex,
                request.PageSize);

            var result = _mapper.Map<PagedResult<Response.ProductResponse>>(products);
            return Result.Success(result);
        }
    }

    private static Expression<Func<AppUser, object>> GetSortProperty(GetEmployeesQuery request)
         => request.SortColoumn?.ToLower() switch
         {
             "EmployeeCode" => Users => Users.EmploeeyCode,
             "Email" => Users => Users.Email,
             "FirstName" => Users => Users.FirstName,
             "LastName" => Users => Users.LastName,
             "FullName" => Users => Users.FullName,
             "IdentityNumber" => Users => Users.IdentityNumber,
             "PhoneNumber" => Users => Users.PhoneNumber,
             "TaxCode" => Users => Users.TaxCode,
             "BankAccount" => Users => Users.BankAccount,
             "AddressId" => Users => Users.AddressId,
             "DepartmentId" => Users => Users.DepartmentId,
             "PositionId" => Users => Users.PositionId,
             "DateOfBirth" => Users => Users.DateOfBirth,
             _=> product => product.Id
             //_ => product => product.CreatedDate // Default Sort Descending on CreatedDate column
         };
}


