using EXE201_BE_ThrivoHR.Domain.Entities.Forms;
using EXE201_BE_ThrivoHR.Domain.Repositories;
using EXE201_BE_ThrivoHR.Infrastructure.Persistence;

namespace EXE201_BE_ThrivoHR.Infrastructure.Repositories;

public class ResignFormRepository(ApplicationDbContext dbContext, IMapper mapper) : RepositoryBase<ResignForm, ResignForm, ApplicationDbContext>(dbContext, mapper), IResignFormRepository;
