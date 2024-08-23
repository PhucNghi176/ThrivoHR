using FluentValidation;

namespace EXE201_BE_ThrivoHR.Application.Common.Validation;

public interface IValidatorProvider
{
    IValidator<T> GetValidator<T>();
}
