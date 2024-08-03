using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace EXE201_BE_ThrivoHR.Application.Common.Validation
{
    public class ValidatorProvider(IServiceProvider serviceProvider) : IValidatorProvider
    {
        public IValidator<T> GetValidator<T>()
        {
            return serviceProvider.GetService<IValidator<T>>()!;
        }
    }
}
