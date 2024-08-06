using EXE201_BE_ThrivoHR.Domain.Entities.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
namespace EXE201_BE_ThrivoHR.Domain.Repositories;

public interface IUserRepository : IEFRepository<AppUser, AppUser>
{
    Task<bool> VerifyPasswordAsync(string password, string passwordHash);

}
