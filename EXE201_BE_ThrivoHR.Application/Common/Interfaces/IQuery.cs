using EXE201_BE_ThrivoHR.Application.Common.Models;
using MediatR;

namespace EXE201_BE_ThrivoHR.Application.Common.Interfaces;

public interface IQuery<TResponse> : IRequest<Result<TResponse>>
{ }
