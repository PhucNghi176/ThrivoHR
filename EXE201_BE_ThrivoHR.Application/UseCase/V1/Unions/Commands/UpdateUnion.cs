using EXE201_BE_ThrivoHR.Application.Model;
using EXE201_BE_ThrivoHR.Application.Common.Exceptions;
using EXE201_BE_ThrivoHR.Application.Common.Method;
using EXE201_BE_ThrivoHR.Domain.Entities;
using EXE201_BE_ThrivoHR.Domain.Common.Exceptions;

namespace EXE201_BE_ThrivoHR.Application.UseCase.V1.Unions.Commands;

public record UpdateUnion(UnionModel UnionModel, int ID) : ICommand;
internal sealed class UpdateUnionHandler : ICommandHandler<UpdateUnion>
{
    private readonly IUnionRepository _unionRepository;
    private readonly IMapper _mapper;
    private readonly IEmployeeRepository _employeeRepository;

    public UpdateUnionHandler(IUnionRepository unionRepository, IMapper mapper, IEmployeeRepository employeeRepository)
    {
        _unionRepository = unionRepository;
        _mapper = mapper;
        _employeeRepository = employeeRepository;
    }

    public async Task<Result> Handle(UpdateUnion request, CancellationToken cancellationToken)
    {
        var u = await _unionRepository.FindAsync(x => x.Id == request.ID, cancellationToken) ?? throw new NotFoundException(request.ID.ToString());
        u = _mapper.Map(request.UnionModel, u);

        await _unionRepository.UpdateAsync(u);

        await _unionRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
        return Result.Success();

    }
}
