using EXE201_BE_ThrivoHR.Application.Model;
using EXE201_BE_ThrivoHR.Domain.Entities.Forms;

namespace EXE201_BE_ThrivoHR.Application.UseCase.V1.Forms.ApplicationForms.Commands;

public record CreateApplicationForms(ApplicationFormModel ApplicationFormModel) : ICommand;
internal sealed class CreateApplicationFormsHandler : ICommandHandler<CreateApplicationForms>
{
    private readonly IApplicationFormRepository _applicationFormRepository;
    private readonly IMapper _mapper;

    public CreateApplicationFormsHandler(IApplicationFormRepository applicationFormRepository, IMapper mapper)
    {
        _applicationFormRepository = applicationFormRepository;
        _mapper = mapper;
    }

    public async Task<Result> Handle(CreateApplicationForms request, CancellationToken cancellationToken)
    {

        await _applicationFormRepository.AddAsync(_mapper.Map<ApplicationForm>(request.ApplicationFormModel));
        await _applicationFormRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
        return Result.Success();

    }
}
