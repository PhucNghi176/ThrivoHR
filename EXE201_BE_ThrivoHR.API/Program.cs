using AspNetCoreRateLimit;
using EXE201_BE_ThrivoHR.API.Configuration;
using EXE201_BE_ThrivoHR.API.Filters;
using EXE201_BE_ThrivoHR.Application;
using EXE201_BE_ThrivoHR.Infrastructure;

using Serilog;
using System.Text.Json;

// Create the builder
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

// Run the app
await app.RunAsync();
