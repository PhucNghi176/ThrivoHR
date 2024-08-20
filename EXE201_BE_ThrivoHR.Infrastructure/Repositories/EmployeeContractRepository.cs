using EXE201_BE_ThrivoHR.Domain.Entities.Contracts;
using EXE201_BE_ThrivoHR.Domain.Repositories;
using EXE201_BE_ThrivoHR.Infrastructure.Persistence;

namespace EXE201_BE_ThrivoHR.Infrastructure.Repositories;

public class EmployeeContractRepository(ApplicationDbContext dbContext, IMapper mapper) : RepositoryBase<EmployeeContract, EmployeeContract, ApplicationDbContext>(dbContext, mapper), IEmployeeContractRepository;

