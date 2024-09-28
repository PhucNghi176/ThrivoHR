using EXE201_BE_ThrivoHR.Domain.Entities;
using EXE201_BE_ThrivoHR.Domain.Repositories;
using EXE201_BE_ThrivoHR.Infrastructure.Persistence;

namespace EXE201_BE_ThrivoHR.Infrastructure.Repositories;

public class EmployeesProjectMappingRepository(ApplicationDbContext dbContext, IMapper mapper) : RepositoryBase<EmployeesProjectMapping, EmployeesProjectMapping, ApplicationDbContext>(dbContext, mapper),IEmployeesProjectMappingRepository;

