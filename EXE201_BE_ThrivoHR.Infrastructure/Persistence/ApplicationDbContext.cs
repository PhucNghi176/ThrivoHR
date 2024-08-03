using EXE201_BE_ThrivoHR.Domain.Common.Interfaces;
using EXE201_BE_ThrivoHR.Domain.Entities.Identity;
using EXE201_BE_ThrivoHR.Infrastructure.Configurations;
using Microsoft.EntityFrameworkCore;
using Action = EXE201_BE_ThrivoHR.Domain.Entities.Identity.Action;
namespace EXE201_BE_ThrivoHR.Infrastructure.Persistence
{
    public class ApplicationDbContext : DbContext, IUnitOfWork
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

      

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new ActionConfiguration());
            modelBuilder.ApplyConfiguration(new ActionInFunctionConfiguration());
            modelBuilder.ApplyConfiguration(new PermissionConfiguration());
            modelBuilder.ApplyConfiguration(new FunctionConfiguration());
            modelBuilder.ApplyConfiguration(new AppRoleClaimConfiguration());
            modelBuilder.ApplyConfiguration(new AppUserConfiguration());
            
            ConfigureModel(modelBuilder);
            
        }

        public DbSet<AppUser> AppUses { get; set; }
        public DbSet<Action> Actions { get; set; }
        public DbSet<Function> Functions { get; set; }
        public DbSet<ActionInFunction> ActionInFunctions { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        private void ConfigureModel(ModelBuilder modelBuilder)
        {
            
        }
    }
}
