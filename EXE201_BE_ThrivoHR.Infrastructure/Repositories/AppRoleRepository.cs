using EXE201_BE_ThrivoHR.Domain.Entities.Identity;
using EXE201_BE_ThrivoHR.Domain.Repositories;
using EXE201_BE_ThrivoHR.Infrastructure.Persistence;

namespace EXE201_BE_ThrivoHR.Infrastructure.Repositories;

public class AppRoleRepository : RepositoryBase<AppRole, AppRole, ApplicationDbContext>, IAppRoleRepository
{
    public AppRoleRepository(ApplicationDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
    {
    }
}
