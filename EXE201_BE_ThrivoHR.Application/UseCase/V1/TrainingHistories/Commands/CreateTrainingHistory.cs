using EXE201_BE_ThrivoHR.Application.Common.Exceptions;
using EXE201_BE_ThrivoHR.Application.Common.Method;
using EXE201_BE_ThrivoHR.Application.Model;
using EXE201_BE_ThrivoHR.Domain.Entities;

namespace EXE201_BE_ThrivoHR.Application.UseCase.V1.TrainingHistories.Commands;

public record CreateTrainingHistory(TrainingHistoryModelCreate TrainingHistoryModelCreate) : ICommand;
internal sealed class CreateTrainingHistoryHandler(IEmployeeRepository employeeRepository, ITrainingHistoryRepository trainingHistoryRepository, IMapper mapper) : ICommandHandler<CreateTrainingHistory>
{
    private readonly IEmployeeRepository _employeeRepository = employeeRepository;
    private readonly ITrainingHistoryRepository _trainingHistoryRepository = trainingHistoryRepository;
    private readonly IMapper _mapper = mapper;

    public async Task<Result> Handle(CreateTrainingHistory request, CancellationToken cancellationToken)
    {
        var Employee = await _employeeRepository.FindAsync(x => x.EmployeeId == EmployeesMethod.ConvertEmployeeCodeToId(request.TrainingHistoryModelCreate.EmployeeCode!), cancellationToken) ?? throw new Employee.NotFoundException(request.TrainingHistoryModelCreate.EmployeeCode);

        var history = _mapper.Map<TrainingHistory>(request.TrainingHistoryModelCreate);
        history.EmployeeId = Employee.Id;
        await _trainingHistoryRepository.AddAsync(history);

        await _trainingHistoryRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
        return Result.Success();

    }
}
