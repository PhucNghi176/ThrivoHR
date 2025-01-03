﻿using CloudinaryDotNet;
using EXE201_BE_ThrivoHR.Domain.Common.Interfaces;
using EXE201_BE_ThrivoHR.Domain.Repositories;
using EXE201_BE_ThrivoHR.Infrastructure.BackgroundJobs;
using EXE201_BE_ThrivoHR.Infrastructure.Persistence;
using EXE201_BE_ThrivoHR.Infrastructure.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Quartz;

namespace EXE201_BE_ThrivoHR.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>((sp, options) =>
        {
            options.UseSqlServer(
                configuration.GetConnectionString("Azure"),
                b =>
                {
                    b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName);
                    b.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
                });
            options.UseLazyLoadingProxies();
        });
        services.AddScoped<IUnitOfWork>(provider => provider.GetRequiredService<ApplicationDbContext>());
        services.AddScoped<IEmployeeRepository, EmployeeRepository>();
        services.AddScoped<IPositionRepository, PositionRepository>();
        services.AddScoped<IDepartmentRepository, DepartmentRepository>();
        services.AddScoped<IAddressRepository, AddressRepository>();
        services.AddScoped<IAppRoleRepository, AppRoleRepository>();
        services.AddScoped<IEmployeeContractRepository, EmployeeContractRepository>();
        services.AddScoped<ITrainingHistoryRepository, TrainingHistoryRepository>();
        services.AddScoped<IApplicationFormRepository, ApplicationFormRepository>();
        services.AddScoped<IResignFormRepository, ResignFormRepository>();
        services.AddScoped<IUnionRepository, UnionRepository>();
        services.AddScoped<IAbsentFormRepository, AbsentFormRepository>();
        services.AddScoped<IRewardAndDisciplinaryRepository, RewardAndDisciplinaryRepository>();
        services.AddScoped<IOvertimeRepository, OvertimeRepository>();
        services.AddScoped<IProjectRepository, ProjectRepository>();
        services.AddScoped<ITaskHistoryRepository, TaskHistoryRepository>();
        services.AddScoped<IProjectTaskRepository, ProjectTaskRepository>();
        services.AddScoped<IEmployeesProjectMappingRepository, EmployeesProjectMappingRepository>();
        services.AddScoped<ISalaryRepository, SalaryRepository>();
        services.AddScoped<ISystemConfigRepository, SystemConfigRepository>();
        services.AddScoped<IFaceDetectionRepository, FaceDetectionRepository>();
        services.AddScoped<IAttendanceRepository, AttendanceRepository>();
        return services;
    }

    [Obsolete("Obsolete")]
    public static void AddQuartInfrastructure(this IServiceCollection services)
    {
        services.AddQuartz(q =>
        {
            var jobKey = new JobKey(nameof(IncreaseLeaveDayBackgroundJob));
            q.AddJob<IncreaseLeaveDayBackgroundJob>(jobKey).AddTrigger(t => t.ForJob(jobKey).WithCronSchedule("0 0 12 1 1/1 ? *"));
            q.UseMicrosoftDependencyInjectionJobFactory();

        });
        services.AddQuartzHostedService();
    }
    public static void AddCloudinaryInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        Cloudinary cloudinary = new(configuration.GetSection("CLOUDINARY_URL").Value)
        {
            Api =
            {
                Secure = true
            }
        };
        services.AddSingleton(cloudinary);
    }
}