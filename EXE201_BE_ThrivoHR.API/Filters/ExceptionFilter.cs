using EXE201_BE_ThrivoHR.Application.Common.Exceptions;
using EXE201_BE_ThrivoHR.Domain.Common.Exceptions;
using FluentValidation;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace EXE201_BE_ThrivoHR.API.Filters;

public class ExceptionFilter : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        switch (context.Exception)
        {
            case ValidationException exception:
                foreach (var error in exception.Errors)
                {
                    context.ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }
                context.Result = new UnprocessableEntityObjectResult(new ValidationProblemDetails(context.ModelState))
                .AddContextInformation(context);
                context.ExceptionHandled = true;
                break;
            case ForbiddenAccessException:
                context.Result = new ForbidResult();
                context.ExceptionHandled = true;
                break;
            case UnauthorizedAccessException:
            case NotFoundException:
            case BadRequestException:
                var ex = context.Exception;
                context.Result = new ObjectResult(new ProblemDetails
                {
                    Detail = ex?.Message
                })
                {
                    StatusCode = context.Exception switch
                    {
                        UnauthorizedAccessException => 401, // Unauthorized
                        NotFoundException => 404, // Not Found
                        BadRequestException => 400, // Bad Request
                        _ => 500 // Internal Server Error fallback
                    }
                }
                .AddContextInformation(context);
                context.ExceptionHandled = true;
                break;
            case DbUpdateException exception:
                context.Result = new ObjectResult(new ProblemDetails
                {
                    Detail = exception.InnerException?.Message
                })
                {
                    StatusCode = 422
                }
                .AddContextInformation(context);
                context.ExceptionHandled = true;
                break;
            default:
                context.Result = new ObjectResult(new ProblemDetails
                {
                    Detail = context.Exception.Message
                })
                {
                    StatusCode = 500
                }
                .AddContextInformation(context);
                context.ExceptionHandled = true;
                break;



        }
    }
}
internal static class ProblemDetailsExtensions
{
    public static IActionResult AddContextInformation(this ObjectResult objectResult, ExceptionContext context)
    {
        if (objectResult.Value is not ProblemDetails problemDetails)
        {
            return objectResult;
        }
        problemDetails.Extensions.Add("traceId", Activity.Current?.Id ?? context.HttpContext.TraceIdentifier);
        return objectResult;
    }
}
