using AutoMapper;
using EXE201_BE_ThrivoHR.Domain.Entities.Identity;
using EXE201_BE_ThrivoHR.Domain.Repositories;
using EXE201_BE_ThrivoHR.Infrastructure.Persistence;

namespace EXE201_BE_ThrivoHR.Infrastructure.Repositories;

public class UserRepository(ApplicationDbContext dbContext, IMapper mapper) : RepositoryBase<AppUser, AppUser, ApplicationDbContext>(dbContext, mapper), IUserRepository
{
}
