using EXE201_BE_ThrivoHR.API.Configuration;
using EXE201_BE_ThrivoHR.API.Filters;
using EXE201_BE_ThrivoHR.Application;
using EXE201_BE_ThrivoHR.Infrastructure;
using Serilog;

// Create the builder
WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

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
    _ = opt.Filters.Add<ExceptionFilter>();
});
builder.Services.AddApplication(); // Note: 'Configuration' is available on the builder
builder.Services.ConfigureApplicationSecurity(builder.Configuration);
builder.Services.ConfigureProblemDetails();
builder.Services.ConfigureApiVersioning();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.ConfigureSwagger();

//allow all cors
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
               builder =>
               {
                   _ = builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
               });
});
// Build the app
WebApplication app = builder.Build();

// Configure the middleware pipeline
if (app.Environment.IsDevelopment())
{
    _ = app.UseDeveloperExceptionPage();
}
app.UseSerilogRequestLogging();
app.UseExceptionHandler();
app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseCors();
app.UseEndpoints(endpoints =>
{
    _ = endpoints.MapControllers();
});
app.UseSwashbuckle();
await app.RunAsync();
