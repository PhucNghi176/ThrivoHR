using Marvin.Cache.Headers;

namespace EXE201_BE_ThrivoHR.API.Configuration;

public static class HttpCacheHeaders
{
    public static IServiceCollection HttpCacheHeadersConfiguration(this IServiceCollection services)
    {
        services.AddHttpCacheHeaders(
            (expirationModelOptions) =>
            {
                expirationModelOptions.MaxAge = 65;
                expirationModelOptions.CacheLocation = CacheLocation.Private;
            },
            (validationModelOptions) =>
            {
                validationModelOptions.MustRevalidate = true;
            });

        return services;
    }

}
