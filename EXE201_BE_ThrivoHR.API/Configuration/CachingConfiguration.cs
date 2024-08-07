namespace EXE201_BE_ThrivoHR.API.Configuration;

public static class CachingConfiguration
{
    public static IServiceCollection ConfigureCaching(this IServiceCollection services)
    {
        services.AddResponseCaching();
        return services;
    }
}
