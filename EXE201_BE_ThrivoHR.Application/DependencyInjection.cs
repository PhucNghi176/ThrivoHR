using EXE201_BE_ThrivoHR.Application.Common.Behaviours;
using EXE201_BE_ThrivoHR.Application.Common.Validation;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;


namespace EXE201_BE_ThrivoHR.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly(), ServiceLifetime.Transient);
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            //  cfg.AddOpenBehavior(typeof(UnhandledExceptionBehaviour<,>));
            //   cfg.AddOpenBehavior(typeof(PerformanceBehaviour<,>));
            cfg.AddOpenBehavior(typeof(AuthorizationBehaviour<,>));
            cfg.AddOpenBehavior(typeof(ValidationBehaviour<,>));
            cfg.AddOpenBehavior(typeof(UnitOfWorkBehaviour<,>));
        });
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddScoped<IValidatorProvider, ValidatorProvider>();
        return services;
    }
}

