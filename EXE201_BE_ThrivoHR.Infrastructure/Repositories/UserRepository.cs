using EXE201_BE_ThrivoHR.Domain.Entities.Identity;
using EXE201_BE_ThrivoHR.Domain.Repositories;
using EXE201_BE_ThrivoHR.Infrastructure.Persistence;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace EXE201_BE_ThrivoHR.Infrastructure.Repositories;

public class UserRepository(ApplicationDbContext dbContext, IMapper mapper) : RepositoryBase<AppUser, AppUser, ApplicationDbContext>(dbContext, mapper), IUserRepository
{
    

    public Task<bool> VerifyPasswordAsync(string password, string passwordHash)
    => Task.FromResult(BCrypt.Net.BCrypt.Verify(password, passwordHash));
}
