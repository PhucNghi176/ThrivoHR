using EXE201_BE_ThrivoHR.Domain.Entities.Forms;
using EXE201_BE_ThrivoHR.Domain.Repositories;
using EXE201_BE_ThrivoHR.Infrastructure.Persistence;

namespace EXE201_BE_ThrivoHR.Infrastructure.Repositories;

public class ApplicationFormRepository(ApplicationDbContext dbContext, IMapper mapper) : RepositoryBase<ApplicationForm, ApplicationForm, ApplicationDbContext>(dbContext, mapper), IApplicationFormRepository;
