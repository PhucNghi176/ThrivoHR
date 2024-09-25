using EXE201_BE_ThrivoHR.Application.Common.Exceptions;
using EXE201_BE_ThrivoHR.Application.Common.Method;
using EXE201_BE_ThrivoHR.Application.Model;
using EXE201_BE_ThrivoHR.Domain.Entities.Forms;

namespace EXE201_BE_ThrivoHR.Application.UseCase.V1.Forms.AbsentForms.Commands;

public record CreateAbsentForm(AbsentFormModel AbsentFormModel) : ICommand;
internal sealed class CreateAbsentFormHandler(IAbsentFormRepository absentFormRepository, IMapper mapper, IEmployeeRepository employeeRepository) : ICommandHandler<CreateAbsentForm>
{
    public async Task<Result> Handle(CreateAbsentForm request, CancellationToken cancellationToken)
    {
        var Employee = await employeeRepository.FindAsync(x => x.EmployeeId == EmployeesMethod.ConvertEmployeeCodeToId(request.AbsentFormModel.EmployeeCode!), cancellationToken) ?? throw new Employee.NotFoundException(request.AbsentFormModel.EmployeeCode!);
        var model = mapper.Map<AbsentForm>(request.AbsentFormModel);
        model.EmployeeId = Employee.Id;
        await absentFormRepository.AddAsync(model);
        await absentFormRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
        return Result.Success();

    }
}
