using EXE201_BE_ThrivoHR.Domain.Entities;
using EXE201_BE_ThrivoHR.Domain.Repositories;
using EXE201_BE_ThrivoHR.Infrastructure.Persistence;

namespace EXE201_BE_ThrivoHR.Infrastructure.Repositories;

public class SalaryRepository(ApplicationDbContext dbContext, IMapper mapper) : RepositoryBase<Salary, Salary, ApplicationDbContext>(dbContext, mapper), ISalaryRepository;

