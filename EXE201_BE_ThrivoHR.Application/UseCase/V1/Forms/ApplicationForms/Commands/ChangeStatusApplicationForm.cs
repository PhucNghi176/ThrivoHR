
using EXE201_BE_ThrivoHR.Domain.Common.Exceptions;

namespace EXE201_BE_ThrivoHR.Application.UseCase.V1.Forms.ApplicationForms.Commands;

public record ChangeStatusApplicationForm(string Id, Domain.Common.Status.FormStatus Status) : ICommand;
internal sealed class ChangeStatusApplicationFormHanlder : ICommandHandler<ChangeStatusApplicationForm>
{
    private readonly IApplicationFormRepository _applicationFormRepository;

    public ChangeStatusApplicationFormHanlder(IApplicationFormRepository applicationFormRepository)
    {
        _applicationFormRepository = applicationFormRepository;
    }

    public async Task<Result> Handle(ChangeStatusApplicationForm request, CancellationToken cancellationToken)
    {
        var applicationForm = await _applicationFormRepository.FindAsync(x => x.Id == request.Id, cancellationToken) ?? throw new NotFoundException(request.Id);
        applicationForm.Status = request.Status;
        await _applicationFormRepository.UpdateAsync(applicationForm);
        await _applicationFormRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
        return Result.Success();

    }
}
