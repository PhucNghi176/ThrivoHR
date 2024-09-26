using EXE201_BE_ThrivoHR.Domain.Entities;
using EXE201_BE_ThrivoHR.Domain.Repositories;
using EXE201_BE_ThrivoHR.Infrastructure.Persistence;

namespace EXE201_BE_ThrivoHR.Infrastructure.Repositories;

public class OvertimeRepository(ApplicationDbContext dbContext, IMapper mapper) : RepositoryBase<Overtime, Overtime, ApplicationDbContext>(dbContext, mapper), IOvertimeRepository;
