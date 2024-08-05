using MediatR;

namespace EXE201_BE_ThrivoHR.Application.Common.Interfaces;

public interface ICommand : IRequest<Result>
{

}

public interface ICommand<TResponse> : IRequest<Result<TResponse>>
{

}
