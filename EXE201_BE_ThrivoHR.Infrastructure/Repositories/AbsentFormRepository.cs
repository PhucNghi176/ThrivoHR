using EXE201_BE_ThrivoHR.Domain.Entities.Forms;
using EXE201_BE_ThrivoHR.Domain.Repositories;
using EXE201_BE_ThrivoHR.Infrastructure.Persistence;

namespace EXE201_BE_ThrivoHR.Infrastructure.Repositories;

public class AbsentFormRepository : RepositoryBase<AbsentForm, AbsentForm, ApplicationDbContext>, IAbsentFormRepository
{
    public AbsentFormRepository(ApplicationDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
    {
    }
}
