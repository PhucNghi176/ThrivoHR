using EXE201_BE_ThrivoHR.Domain.Common.Interfaces;
using EXE201_BE_ThrivoHR.Domain.Entities;
using EXE201_BE_ThrivoHR.Domain.Entities.Identity;
using Microsoft.EntityFrameworkCore;
using Action = EXE201_BE_ThrivoHR.Domain.Entities.Identity.Action;
namespace EXE201_BE_ThrivoHR.Infrastructure.Persistence
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options), IUnitOfWork
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(AssemblyReference.Assembly);
            ConfigureModel(modelBuilder);

        }

        public DbSet<AppUser> AppUses { get; set; }
        public DbSet<Action> Actions { get; set; }
        public DbSet<Function> Functions { get; set; }
        public DbSet<ActionInFunction> ActionInFunctions { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Address> Addresses { get; set; }
        private void ConfigureModel(ModelBuilder modelBuilder)
        {

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
                    TaxCode = "1",
                    FirstName = "Admin",
                    LastName = "Admin",
                    FullName = "Admin",
                    EmploeeyCode = "0",

                });
            modelBuilder.Entity<Department>().HasData(
                new Department { Id = 1, Name = "HR", Description = "Human Resource" },
                new Department { Id = 2, Name = "IT", Description = "IT" },
                new Department { Id = 3, Name = "Accounting", Description = "Accounting" },
                new Department { Id = 4, Name = "Marketing", Description = "Marketing" },
                new Department { Id = 5, Name = "Sale", Description = "Sale" }

            );
            modelBuilder.Entity<Position>().HasData(
                new Position { Id = 1, Name = "Director", Description = "Director" },
                new Position { Id = 2, Name = "Manager", Description = "Manager" },
                new Position { Id = 3, Name = "Staff", Description = "Staff" },
                new Position { Id = 4, Name = "Intern", Description = "Intern" },
                new Position { Id = 5, Name = "Supervisor", Description = "Supervisor" }
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

        }
    }
}

