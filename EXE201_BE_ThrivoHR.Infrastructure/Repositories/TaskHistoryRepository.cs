using EXE201_BE_ThrivoHR.Domain.Entities;
using EXE201_BE_ThrivoHR.Domain.Repositories;
using EXE201_BE_ThrivoHR.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EXE201_BE_ThrivoHR.Infrastructure.Repositories;

public class TaskHistoryRepository(ApplicationDbContext dbContext, IMapper mapper) : RepositoryBase<TaskHistory, TaskHistory, ApplicationDbContext>(dbContext, mapper), ITaskHistoryRepository;
