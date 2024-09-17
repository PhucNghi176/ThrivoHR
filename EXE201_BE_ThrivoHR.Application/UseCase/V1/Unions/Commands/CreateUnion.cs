using EXE201_BE_ThrivoHR.Application.Common.Exceptions;
using EXE201_BE_ThrivoHR.Application.Common.Method;

using EXE201_BE_ThrivoHR.Application.Model;
using EXE201_BE_ThrivoHR.Domain.Entities;


namespace EXE201_BE_ThrivoHR.Application.UseCase.V1.Unions.Commands;

public record CreateUnion(UnionModel unionModel) : ICommand;
internal sealed class CreateUnionHandler : ICommandHandler<CreateUnion>
{
    private readonly IUnionRepository _unionRepository;
    private readonly IMapper _mapper;
    private readonly IEmployeeRepository _employeeRepository;

    public CreateUnionHandler(IUnionRepository unionRepository, IMapper mapper, IEmployeeRepository employeeRepository)
    {
        _unionRepository = unionRepository;
        _mapper = mapper;
        _employeeRepository = employeeRepository;
    }

    public async Task<Result> Handle(CreateUnion request, CancellationToken cancellationToken)
    {
        var Employee = await _employeeRepository.FindAsync(x => x.EmployeeId == EmployeesMethod.ConvertEmployeeCodeToId(request.unionModel.EmployeeCode!), cancellationToken) ?? throw new Employee.NotFoundException(request.unionModel.EmployeeCode);
        var u = _mapper.Map<Union>(request.unionModel);
        u.EmployeeId = Employee.Id;
        await _unionRepository.AddAsync(u);

        await _unionRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
        return Result.Success();

    }
}
