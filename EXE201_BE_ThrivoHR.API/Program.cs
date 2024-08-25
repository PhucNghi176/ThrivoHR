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
        builder.Services.ConfigureProblemDetails();
        builder.Services.ConfigureApiVersioning();
        builder.Services.AddInfrastructure(builder.Configuration);
        builder.Services.ConfigureSwagger();
        builder.Services.AddResponseCaching();
        builder.Services.HttpCacheHeadersConfiguration();
        builder.Services.ConfigureRateLimit();
        builder.Services.AddSingleton<IRateLimitConfiguration, AspNetCoreRateLimit.RateLimitConfiguration>();

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
        app.UseHttpCacheHeaders();

        app.MapControllers();

        app.UseSwashbuckle();
        app.MapGet("/deploy", async (HttpContext context) =>
        {
            try
            {
                // Change directory
                Directory.SetCurrentDirectory(@"C:\Users\xuanghi\Desktop\EXE201_BE_THRIVOHR");

                // Execute commands
                await ExecuteCommand("git", "pull");
                await ExecuteCommand("dotnet", $"ef migrations add \"migration{DateTime.Now:yyyyMMdd_HHmmss}\" --startup-project \"EXE201_BE_ThrivoHR.API\" --project \"EXE201_BE_ThrivoHR.Infrastructure\"");
                await ExecuteCommand("dotnet", "ef database update --startup-project \"EXE201_BE_ThrivoHR.API\" --project \"EXE201_BE_ThrivoHR.Infrastructure\"");

                Directory.SetCurrentDirectory(@"C:\Users\xuanghi\Desktop\EXE201_BE_THRIVOHR\EXE201_BE_ThrivoHR.API");

                await ExecuteCommand("dotnet", "restore EXE201_BE_ThrivoHR.API.csproj");
                await ExecuteCommand("dotnet", "clean");
                await ExecuteCommand("dotnet", "build --configuration release");
                await ExecuteCommand("dotnet", "publish /p:Configuration=release /p:EnvironmentName=Production");

                await ExecuteCommand(@"%windir%\system32\inetsrv\appcmd", "stop sites ThrivoHR.exe201.com");
                await ExecuteCommand(@"%windir%\system32\inetsrv\appcmd", "stop apppool /apppool.name:ThrivoHR.exe201.com");

                await Task.Delay(5000); // Simulating ping delay

                await ExecuteCommand("xcopy", @"""C:\Users\xuanghi\Desktop\EXE201_BE_THRIVOHR\EXE201_BE_ThrivoHR.API\bin\release\net8.0"" ""C:\WWW\EXE201_BE_ThrivoHR\PROD"" /e /y /i /r");

                await ExecuteCommand(@"%windir%\system32\inetsrv\appcmd", "start apppool /apppool.name:ThrivoHR.exe201.com");
                await ExecuteCommand(@"%windir%\system32\inetsrv\appcmd", "start sites ThrivoHR.exe201.com");

                return Results.Ok("Deployment completed successfully");
            }
            catch (Exception ex)
            {
                return null;
            }
        });

        async Task ExecuteCommand(string command, string arguments)
        {
            using var process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = command,
                    Arguments = arguments,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    CreateNoWindow = true,
                }
            };

            process.Start();
            await process.WaitForExitAsync();

            if (process.ExitCode != 0)
            {
                throw new Exception($"Command failed: {command} {arguments}");
            }
        }

        // Run the app
        await app.RunAsync();
    }
}
