using EXE201_BE_ThrivoHR.Domain.Entities;
using EXE201_BE_ThrivoHR.Domain.Repositories;
using EXE201_BE_ThrivoHR.Infrastructure.Persistence;

namespace EXE201_BE_ThrivoHR.Infrastructure.Repositories;

public class ProjectRepository(ApplicationDbContext dbContext, IMapper mapper) : RepositoryBase<Project, Project, ApplicationDbContext>(dbContext, mapper), IProjectRepository;
