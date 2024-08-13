using EXE201_BE_ThrivoHR.Domain.Entities.Identity;
namespace EXE201_BE_ThrivoHR.Domain.Repositories;

public interface IUserRepository : IEFRepository<AppUser, AppUser>
{
    Task<bool> VerifyPasswordAsync(string password, string passwordHash);

}
