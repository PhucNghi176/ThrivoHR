using MediatR;

namespace EXE201_BE_ThrivoHR.Application.Common.Models;

public interface IQuery<TResponse> : IRequest<Result<TResponse>>
{ }
