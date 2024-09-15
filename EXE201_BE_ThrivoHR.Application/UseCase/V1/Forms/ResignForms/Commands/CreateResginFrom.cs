using EXE201_BE_ThrivoHR.Application.Common.Exceptions;
using EXE201_BE_ThrivoHR.Application.Common.Method;
using EXE201_BE_ThrivoHR.Application.Model;
using EXE201_BE_ThrivoHR.Domain.Entities.Forms;

namespace EXE201_BE_ThrivoHR.Application.UseCase.V1.Forms.ResignForms.Commands;

public record CreateResginForm(ResginFormModel ResginModel) : ICommand;
internal sealed class CreateResginFormHandler : ICommandHandler<CreateResginForm>
{
    private readonly IResignFormRepository _resignFormRepository;
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IMapper _mapper;

    public CreateResginFormHandler(IResignFormRepository resignFormRepository, IMapper mapper, IEmployeeRepository employeeRepository)
    {
        _resignFormRepository = resignFormRepository;
        _mapper = mapper;
        _employeeRepository = employeeRepository;
    }

    public async Task<Result> Handle(CreateResginForm request, CancellationToken cancellationToken)
    {
        var employee = await _employeeRepository.FindAsync(x => x.EmployeeId == EmployeesMethod.ConvertEmployeeCodeToId(request.ResginModel.EmployeeCode!), cancellationToken) ?? throw new Employee.NotFoundException(request.ResginModel.EmployeeCode!);
        var form = _mapper.Map<ResignForm>(request.ResginModel);
        form.EmployeeId = employee.Id;
        await _resignFormRepository.AddAsync(form);
        await _resignFormRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
        return Result.Success();

    }
}
