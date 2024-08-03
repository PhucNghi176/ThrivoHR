using MediatR;

namespace EXE201_BE_ThrivoHR.Application.Common.Models;

public interface IQueryHandler<TQuery, TResponse> : IRequestHandler<TQuery, Result<TResponse>>
    where TQuery : IQuery<TResponse>
{ }
