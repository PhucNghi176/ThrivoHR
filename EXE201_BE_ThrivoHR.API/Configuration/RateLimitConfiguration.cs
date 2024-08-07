﻿using AspNetCoreRateLimit;

namespace EXE201_BE_ThrivoHR.API.Configuration;

public static class RateLimitConfiguration
{
    public static IServiceCollection ConfigureRateLimit(this IServiceCollection services)
    {
        services.AddMemoryCache();

        services.Configure<IpRateLimitOptions>(options =>
        {
            options.GeneralRules =
            [
                new() {
                    Endpoint = "*",
                    Limit = 100,
                    Period = "1m"
                }

            ];
        });

        services.AddSingleton<IRateLimitCounterStore, MemoryCacheRateLimitCounterStore>();
        services.AddSingleton<IIpPolicyStore, MemoryCacheIpPolicyStore>();
        services.AddSingleton<IProcessingStrategy, AsyncKeyLockProcessingStrategy>();

        return services;
    }
}
