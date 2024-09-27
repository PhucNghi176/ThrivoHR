using AspNetCoreRateLimit;
using EXE201_BE_ThrivoHR.API.Configuration;
using EXE201_BE_ThrivoHR.API.Filters;
using EXE201_BE_ThrivoHR.Application;
using EXE201_BE_ThrivoHR.Infrastructure;
using Serilog;
using System.Diagnostics;

namespace EXE201_BE_ThrivoHR.API;

public static class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        IronXL.License.LicenseKey = "IRONSUITE.NGHIPPSE173558.FPT.EDU.VN.14754-17AE407732-DUCJQEVEGCANXU-5SRBWUGKIDUY-UMUF2MNT2VYS-ZBJ7SOYX6QNW-LZJDAIB7FMRT-C5BDIPIMX2LS-AG6RJK-TYN7QZ6WELSNUA-DEPLOYMENT.TRIAL-5MEDKM.TRIAL.EXPIRES.17.OCT.2024";
        // Configure logging (Serilog)
        builder.Host.UseSerilog((context, services, configuration) => configuration
            .ReadFrom.Configuration(context.Configuration)
            .ReadFrom.Services(services)
            .Enrich.FromLogContext()
            .WriteTo.File("Logs/logs.txt", rollingInterval: RollingInterval.Day)
            .WriteTo.Console());

        // Add services
        builder.Services.AddControllers(opt =>
        {
            opt.Filters.Add<ExceptionFilter>();
        }).AddNewtonsoftJson();

        builder.Services.AddApplication(); // Assuming this registers your application services
        builder.Services.ConfigureApplicationSecurity(builder.Configuration);
        // builder.Services.ConfigureProblemDetails();
        builder.Services.ConfigureApiVersioning();
        builder.Services.AddInfrastructure(builder.Configuration);
        builder.Services.ConfigureSwagger();
          builder.Services.AddResponseCaching();
        //builder.Services.HttpCacheHeadersConfiguration();
        builder.Services.ConfigureRateLimit();
        builder.Services.AddSingleton<IRateLimitConfiguration, AspNetCoreRateLimit.RateLimitConfiguration>();
        builder.Services.AddQuartInfrastructure();
        builder.Services.AddCloudinaryInfrastructure(builder.Configuration);
        // Allow all CORS
        builder.Services.AddCors(options =>
        {
            options.AddDefaultPolicy(
                builder =>
                {
                    builder.AllowAnyOrigin()
                           .AllowAnyMethod()
                           .AllowAnyHeader();
                });
        });

        // Build the app
        var app = builder.Build();

        // Configure the middleware pipeline
        if (app.Environment.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        else
        {
            app.UseExceptionHandler("/Home/Error");
            app.UseHsts();
        }
        app.UseSerilogRequestLogging();
        app.UseHttpsRedirection();
        app.UseStaticFiles();
        app.UseRouting();
        app.UseCors();
        app.UseAuthentication();
        app.UseAuthorization();
        app.UseIpRateLimiting();
        app.UseResponseCaching();
        //app.UseHttpCacheHeaders();

        app.MapControllers();

        app.UseSwashbuckle();
        // Run the app
        await app.RunAsync();
    }
}
