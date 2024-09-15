using EXE201_BE_ThrivoHR.Application.Common.Exceptions;
using EXE201_BE_ThrivoHR.Domain.Common.Exceptions;
using EXE201_BE_ThrivoHR.Domain.Common.Status;
using EXE201_BE_ThrivoHR.Domain.Services;

namespace EXE201_BE_ThrivoHR.Application.UseCase.V1.Forms.ResignForms.Commands;

public record ChangeStatusResignForm(FormStatus FormStatus, string FormID) : ICommand;
internal sealed class ChangeStatusResignFormHandler : ICommandHandler<ChangeStatusResignForm>
{
    private readonly IResignFormRepository _resignFormRepository;
    private readonly IEmployeeRepository _employeeRepository;
    private readonly ICurrentUserService _currentUserService;

    public ChangeStatusResignFormHandler(IResignFormRepository resignFormRepository, IEmployeeRepository employeeRepository, ICurrentUserService currentUserService)
    {
        _resignFormRepository = resignFormRepository;
        _employeeRepository = employeeRepository;
        _currentUserService = currentUserService;
    }

    public async Task<Result> Handle(ChangeStatusResignForm request, CancellationToken cancellationToken)
    {
        var form = await _resignFormRepository.FindAsync(x => x.Id == request.FormID, cancellationToken) ?? throw new NotFoundException(request.FormID);
        form.Status = request.FormStatus;
        var employee = await _employeeRepository.FindAsync(x => x.Id == form.EmployeeId, cancellationToken) ?? throw new Employee.NotFoundException(form.EmployeeId!);
        if (request.FormStatus == FormStatus.Approved)
        {
            employee.IsDeleted = true;
        }
        form.ApproverId = _currentUserService.UserId;
        await _employeeRepository.UpdateAsync(employee);
        await _resignFormRepository.UpdateAsync(form);
        await _resignFormRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
        return Result.Success();
    }
}
