using EXE201_BE_ThrivoHR.Application.Common.Exceptions;
using EXE201_BE_ThrivoHR.Application.Common.Method;
using EXE201_BE_ThrivoHR.Application.Model;
using EXE201_BE_ThrivoHR.Domain.Entities.Forms;

namespace EXE201_BE_ThrivoHR.Application.UseCase.V1.Forms.AbsentForms.Commands;

public record CreateAbsentForm(AbsentFormModel AbsentFormModel) : ICommand;
internal sealed class CreateAbsentFormHandler : ICommandHandler<CreateAbsentForm>
{
    private readonly IAbsentFormRepository _absentFormRepository;
    private readonly IMapper _mapper;
    private readonly IEmployeeRepository _employeeRepository;

    public CreateAbsentFormHandler(IAbsentFormRepository absentFormRepository, IMapper mapper, IEmployeeRepository employeeRepository)
    {
        _absentFormRepository = absentFormRepository;
        _mapper = mapper;
        _employeeRepository = employeeRepository;
    }

    public async Task<Result> Handle(CreateAbsentForm request, CancellationToken cancellationToken)
    {
        var Employee = await _employeeRepository.FindAsync(x => x.EmployeeId == EmployeesMethod.ConvertEmployeeCodeToId(request.AbsentFormModel.EmployeeCode!), cancellationToken) ?? throw new Employee.NotFoundException(request.AbsentFormModel.EmployeeCode!);
        var mapper = _mapper.Map<AbsentForm>(request.AbsentFormModel);
        mapper.EmployeeId = Employee.Id;
        await _absentFormRepository.AddAsync(mapper);
        await _absentFormRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
        return Result.Success();

    }
}
