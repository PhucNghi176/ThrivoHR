using EXE201_BE_ThrivoHR.Application.Model;
using EXE201_BE_ThrivoHR.Domain.Common.Exceptions;

namespace EXE201_BE_ThrivoHR.Application.UseCase.V1.Forms.AbsentForms.Commands;

public record UpdateAbsentForm(AbsentFormModel AbsentFormModel, string ID) : ICommand<string>;
internal sealed class UpdateAbsentFormHandler(IAbsentFormRepository _absentFormRepository, IMapper _mapper) : ICommandHandler<UpdateAbsentForm, string>
{
    public async Task<Result<string>> Handle(UpdateAbsentForm request, CancellationToken cancellationToken)
    {
        var form = await _absentFormRepository.FindAsync(x => x.Id == request.ID, cancellationToken) ?? throw new NotFoundException("Not found");
        _mapper.Map(request.AbsentFormModel, form);
        await _absentFormRepository.UpdateAsync(form);
        await _absentFormRepository.UnitOfWork.SaveChangesAsync(cancellationToken);


        return Result.Success("Update Absent Form Success");
    }
}

