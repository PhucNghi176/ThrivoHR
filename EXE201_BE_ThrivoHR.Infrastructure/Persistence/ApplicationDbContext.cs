using EXE201_BE_ThrivoHR.Domain.Common.Interfaces;
using EXE201_BE_ThrivoHR.Domain.Entities;
using EXE201_BE_ThrivoHR.Domain.Entities.Base;
using EXE201_BE_ThrivoHR.Domain.Entities.Base.Contract;
using EXE201_BE_ThrivoHR.Domain.Entities.Contracts;
using EXE201_BE_ThrivoHR.Domain.Entities.Forms;
using EXE201_BE_ThrivoHR.Domain.Entities.Identity;
using EXE201_BE_ThrivoHR.Domain.Services;
using Microsoft.AspNetCore.Identity;
using Action = EXE201_BE_ThrivoHR.Domain.Entities.Identity.Action;
namespace EXE201_BE_ThrivoHR.Infrastructure.Persistence;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, ICurrentUserService _currentUserService) : DbContext(options), IUnitOfWork
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(AssemblyReference.Assembly);
        modelBuilder.Entity<AppUser>().HasQueryFilter(x => !x.IsDeleted);
        modelBuilder.Entity<BaseContract>().HasQueryFilter(x => !x.IsDeleted);
        modelBuilder.Entity<Department>().HasQueryFilter(x => !x.IsDeleted);
        modelBuilder.Entity<Position>().HasQueryFilter(x => !x.IsDeleted);
        modelBuilder.Entity<Address>().HasQueryFilter(x => !x.IsDeleted);
        modelBuilder.Entity<BaseForm>().HasQueryFilter(x => !x.IsDeleted);
        modelBuilder.Entity<Union>().HasQueryFilter(x => !x.IsDeleted);
        modelBuilder.Entity<RewardsAndDisciplinary>().HasQueryFilter(x => !x.IsDeleted);
        modelBuilder.Entity<TrainingHistory>().HasQueryFilter(x => !x.IsDeleted);
        modelBuilder.Entity<Overtime>().HasQueryFilter(x => !x.IsDeleted);
        modelBuilder.Entity<ProjectTask>().HasQueryFilter(x => !x.IsDeleted);
        modelBuilder.Entity<Project>().HasQueryFilter(x => !x.IsDeleted);
        modelBuilder.Entity<TaskHistory>().HasQueryFilter(x => !x.IsDeleted);
        modelBuilder.Entity<EmployeesProjectMapping>().HasQueryFilter(x => !x.IsDeleted);
        modelBuilder.Entity<ProjectTask>().ToTable(x => x.HasTrigger("UpdateProjectProgress"));
        modelBuilder.Entity<ProjectTask>().ToTable(x => x.HasTrigger("TrackTaskHistory"));
        modelBuilder.Entity<Salary>().HasQueryFilter(x => !x.IsDeleted);
        modelBuilder.Entity<Attendance>().HasQueryFilter(x=>!x.IsDeleted);
        ConfigureModel(modelBuilder);

    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var entries = ChangeTracker.Entries().Where(e => e.Entity is IAuditableEntity);
        foreach (var entry in entries)
        {
            var auditableEntity = (IAuditableEntity)entry.Entity;
            switch (entry.State)
            {
                case EntityState.Added:
                    auditableEntity.CreatedBy = _currentUserService.UserId;
                    break;
                case EntityState.Modified:
                    if (!auditableEntity.IsDeleted)
                    {
                        auditableEntity.LastModifiedOn = DateTime.UtcNow.AddHours(7);
                        auditableEntity.LastModifiedBy = _currentUserService.UserId;
                    }
                    else
                    {
                        auditableEntity.DeletedOn = DateTime.UtcNow.AddHours(7);
                        auditableEntity.DeletedBy = _currentUserService.UserId;
                        auditableEntity.IsDeleted = true;
                    }
                    break;
                case EntityState.Detached:
                    break;
                case EntityState.Unchanged:
                    break;
                case EntityState.Deleted:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        return await base.SaveChangesAsync(cancellationToken);
    }
    public virtual DbSet<AppUser> AppUses { get; set; }
    public virtual DbSet<Action> Actions { get; set; }
    public virtual DbSet<Function> Functions { get; set; }
    public virtual DbSet<ActionInFunction> ActionInFunctions { get; set; }
    public virtual DbSet<Permission> Permissions { get; set; }
    public virtual DbSet<Position> Positions { get; set; }
    public virtual DbSet<Department> Departments { get; set; }
    public virtual DbSet<Address> Addresses { get; set; }
    public virtual DbSet<EmployeeContract> EmployeeContracts { get; set; }
    public virtual DbSet<TrainingHistory> TrainingHistories { get; set; }
    public virtual DbSet<ApplicationForm> ApplicationForms { get; set; }
    public virtual DbSet<ResignForm> ResginForms { get; set; }
    public virtual DbSet<Union> Unions { get; set; }
    public virtual DbSet<RewardsAndDisciplinary> RewardsAndDisciplinaries { get; set; }
    public virtual DbSet<AbsentForm> AbsentForms { get; set; }
    public virtual DbSet<Overtime> Overtimes { get; set; }
    public virtual DbSet<Project> Projects { get; set; }
    public virtual DbSet<ProjectTask> ProjectTasks { get; set; }
    public virtual DbSet<TaskHistory> TaskHistories { get; set; }
    public virtual DbSet<EmployeesProjectMapping> EmployeesProjectMappings { get; set; }
    public virtual DbSet<Salary> Salaries { get; set; }
    public virtual DbSet<SystemConfig> SystemConfig { get; set; }
    public virtual DbSet<Attendance> Attendances { get; set; }
    private static void ConfigureModel(ModelBuilder modelBuilder)
    {

        modelBuilder.Entity<Department>().HasData(
            new Department { Id = 1, Name = "Admin", Description = "Admin" },
            new Department { Id = 2, Name = "IT", Description = "IT" },
            new Department { Id = 3, Name = "Accounting", Description = "Accounting" },
            new Department { Id = 4, Name = "Marketing", Description = "Marketing" },
            new Department { Id = 5, Name = "Sale", Description = "Sale" },
            new Department { Id = 6, Name = "HR", Description = "Human Resource" }
        );
        modelBuilder.Entity<Position>().HasData(
            new Position { Id = 1, Name = "Admin", Description = "Admin" },
            new Position { Id = 2, Name = "Manager", Description = "Manager" },
            new Position { Id = 3, Name = "Staff", Description = "Staff" },
            new Position { Id = 4, Name = "Intern", Description = "Intern" },
            new Position { Id = 5, Name = "Supervisor", Description = "Supervisor" },
            new Position { Id = 6, Name = "Director", Description = "Director" }
        );
        modelBuilder.Entity<Address>().HasData(
            // TP. Hồ Chí Minh
            new Address { Id = 1, AddressLine = "20, đường 904", Ward = "Hiệp Phú", District = "TP.Thủ Đức", City = "TP.Hồ Chí Minh", Country = "Việt Nam", CreatedBy = "1", LastModifiedBy = "1" },
            new Address { Id = 2, AddressLine = "123, đường Nguyễn Trãi", Ward = "Bến Thành", District = "Quận 1", City = "TP.Hồ Chí Minh", Country = "Việt Nam", CreatedBy = "1", LastModifiedBy = "1" },
            new Address { Id = 3, AddressLine = "456, đường Trần Hưng Đạo", Ward = "Cầu Kho", District = "Quận 1", City = "TP.Hồ Chí Minh", Country = "Việt Nam", CreatedBy = "1", LastModifiedBy = "1" },
            new Address { Id = 4, AddressLine = "789, đường Phạm Ngũ Lão", Ward = "Phạm Ngũ Lão", District = "Quận 1", City = "TP.Hồ Chí Minh", Country = "Việt Nam", CreatedBy = "1", LastModifiedBy = "1" },
            new Address { Id = 5, AddressLine = "101, đường Lê Lợi", Ward = "Bến Nghé", District = "Quận 1", City = "TP.Hồ Chí Minh", Country = "Việt Nam", CreatedBy = "1", LastModifiedBy = "1" },

            // Hà Nội
            new Address { Id = 6, AddressLine = "202, đường Đội Cấn", Ward = "Cống Vị", District = "Ba Đình", City = "Hà Nội", Country = "Việt Nam", CreatedBy = "1", LastModifiedBy = "1" },
            new Address { Id = 7, AddressLine = "303, đường Kim Mã", Ward = "Ngọc Khánh", District = "Ba Đình", City = "Hà Nội", Country = "Việt Nam", CreatedBy = "1", LastModifiedBy = "1" },
            new Address { Id = 8, AddressLine = "404, đường Nguyễn Thái Học", Ward = "Quán Thánh", District = "Ba Đình", City = "Hà Nội", Country = "Việt Nam", CreatedBy = "1", LastModifiedBy = "1" },
            new Address { Id = 9, AddressLine = "505, đường Láng Hạ", Ward = "Láng Hạ", District = "Đống Đa", City = "Hà Nội", Country = "Việt Nam", CreatedBy = "1", LastModifiedBy = "1" },
            new Address { Id = 10, AddressLine = "606, đường Tây Sơn", Ward = "Quang Trung", District = "Đống Đa", City = "Hà Nội", Country = "Việt Nam", CreatedBy = "1", LastModifiedBy = "1" },

            // Đà Nẵng
            new Address { Id = 11, AddressLine = "707, đường Điện Biên Phủ", Ward = "Chính Gián", District = "Thanh Khê", City = "Đà Nẵng", Country = "Việt Nam", CreatedBy = "1", LastModifiedBy = "1" },
            new Address { Id = 12, AddressLine = "808, đường Nguyễn Văn Linh", Ward = "Nam Dương", District = "Hải Châu", City = "Đà Nẵng", Country = "Việt Nam", CreatedBy = "1", LastModifiedBy = "1" },
            new Address { Id = 13, AddressLine = "909, đường Trần Phú", Ward = "Thạch Thang", District = "Hải Châu", City = "Đà Nẵng", Country = "Việt Nam", CreatedBy = "1", LastModifiedBy = "1" },
            new Address { Id = 14, AddressLine = "1010, đường Lê Duẩn", Ward = "Tân Chính", District = "Thanh Khê", City = "Đà Nẵng", Country = "Việt Nam", CreatedBy = "1", LastModifiedBy = "1" },
            new Address { Id = 15, AddressLine = "1111, đường Võ Văn Kiệt", Ward = "Phước Mỹ", District = "Sơn Trà", City = "Đà Nẵng", Country = "Việt Nam", CreatedBy = "1", LastModifiedBy = "1" },

            // Cần Thơ
            new Address { Id = 16, AddressLine = "1212, đường 3/2", Ward = "Xuân Khánh", District = "Ninh Kiều", City = "Cần Thơ", Country = "Việt Nam", CreatedBy = "1", LastModifiedBy = "1" },
            new Address { Id = 17, AddressLine = "1313, đường Nguyễn Trãi", Ward = "An Hội", District = "Ninh Kiều", City = "Cần Thơ", Country = "Việt Nam", CreatedBy = "1", LastModifiedBy = "1" },
            new Address { Id = 18, AddressLine = "1414, đường Trần Hưng Đạo", Ward = "Tân An", District = "Ninh Kiều", City = "Cần Thơ", Country = "Việt Nam", CreatedBy = "1", LastModifiedBy = "1" },
            new Address { Id = 19, AddressLine = "1515, đường Phạm Ngũ Lão", Ward = "An Cư", District = "Ninh Kiều", City = "Cần Thơ", Country = "Việt Nam", CreatedBy = "1", LastModifiedBy = "1" },
            new Address { Id = 20, AddressLine = "1616, đường Lê Lợi", Ward = "An Nghiệp", District = "Ninh Kiều", City = "Cần Thơ", Country = "Việt Nam", CreatedBy = "1", LastModifiedBy = "1" }
        );
        modelBuilder.Entity<AppRole>().HasData(
            new AppRole { Id = "1", Name = "Admin", Description = "Admin", RoleCode = "Admin" },
            new AppRole { Id = "2", Name = "HR", Description = "Human Resource", RoleCode = "HR" },
            new AppRole { Id = "3", Name = "C&B", Description = "Compensation and benefit", RoleCode = "C&B" });

        modelBuilder.Entity<AppUser>().HasData(
            new AppUser
            {
                Id = "1",
                UserName = "admin",
                NormalizedUserName = "ADMIN",
                Email = "Admin@",
                BankAccount = "1",
                IdentityNumber = "1",
                PhoneNumber = "1",
                Religion = "1",
                Ethnicity = "1",
                Sex = true,
                TaxCode = "1",
                FirstName = "Admin",
                LastName = "Admin",
                FullName = "Admin",
                DepartmentId = 1,
                PositionId = 1,
                PasswordHash = "$2a$11$dRZA37NpS.thXR9anJXBZehaTb7ezji2i2E5WbHGA2cwMeW4wEXAy",
                DateOfBirth = new DateOnly(1999, 1, 1),
                AddressId = 1

            });
        // Seeding data for IdentityUserRole<string>
        modelBuilder.Entity<IdentityUserRole<string>>().HasData(
            new IdentityUserRole<string> { RoleId = "1", UserId = "1" }
        );
        modelBuilder.Entity<SystemConfig>().HasData(
            new SystemConfig { Key = "SocialInsurance", Value = 0.08M },
            new SystemConfig { Key = "HealthInsurance", Value = 0.015M },
            new SystemConfig { Key = "UnemploymentInsurance", Value = 0.01M },
            new SystemConfig { Key = "PersonalDeduction", Value = 11000000M },
            new SystemConfig { Key = "SingleDependence", Value = 4400000M },
            new SystemConfig { Key = "TaxabeLevel1Income", Value = 5000000M },
            new SystemConfig { Key = "TaxLevel1", Value = 0.05M },
            new SystemConfig { Key = "TaxLevel1Minus", Value = 0M },
            new SystemConfig { Key = "TaxabeLevel2Income", Value = 10000000M },
            new SystemConfig { Key = "TaxLevel2", Value = 0.1M },
            new SystemConfig { Key = "TaxLevel2Minus", Value = 250000M },
            new SystemConfig { Key = "TaxabeLevel3Income", Value = 18000000M },
            new SystemConfig { Key = "TaxLevel3", Value = 0.15M },
            new SystemConfig { Key = "TaxLevel3Minus", Value = 750000M },
            new SystemConfig { Key = "TaxabeLevel4Income", Value = 32000000M },
            new SystemConfig { Key = "TaxLevel4", Value = 0.2M },
            new SystemConfig { Key = "TaxLevel4Minus", Value = 1650000M },
            new SystemConfig { Key = "TaxabeLevel5Income", Value = 52000000M },
            new SystemConfig { Key = "TaxLevel5", Value = 0.25M },
            new SystemConfig { Key = "TaxLevel5Minus", Value = 3250000M },
            new SystemConfig { Key = "TaxabeLevel6Income", Value = 8000000M },
            new SystemConfig { Key = "TaxLevel6", Value = 0.3M },
            new SystemConfig { Key = "TaxLevel6Minus", Value = 5850000M },
            new SystemConfig { Key = "TaxabeLevel7Income", Value = 80000001M },
            new SystemConfig { Key = "TaxLevel7", Value = 0.35M },
            new SystemConfig { Key = "TaxLevel7Minus", Value = 9850000M },
            new SystemConfig { Key = "BasicSalary", Value = 2340000M },
            new SystemConfig { Key = "MinimumSalaryRegion1", Value = 4680000 }
            );


    }
}

