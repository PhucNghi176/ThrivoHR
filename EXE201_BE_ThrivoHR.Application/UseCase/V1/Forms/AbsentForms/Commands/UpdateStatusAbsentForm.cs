using EXE201_BE_ThrivoHR.Domain.Common.Status;
using static EXE201_BE_ThrivoHR.Application.Common.Exceptions.Employee;

namespace EXE201_BE_ThrivoHR.Application.UseCase.V1.Forms.AbsentForms.Commands;

public record UpdateStatusAbsentForm(string ID, FormStatus Status) : ICommand;
internal sealed class UpdateStatusAbsentFormHandler(IAbsentFormRepository _absentFormRepository) : ICommandHandler<UpdateStatusAbsentForm>
{
    public async Task<Result> Handle(UpdateStatusAbsentForm request, CancellationToken cancellationToken)
    {
        var form = await _absentFormRepository.FindAsync(x => x.Id == request.ID, cancellationToken) ?? throw new NotFoundException("Not found");
        form.Status = request.Status;
        await _absentFormRepository.UpdateAsync(form);
        await _absentFormRepository.UnitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}

