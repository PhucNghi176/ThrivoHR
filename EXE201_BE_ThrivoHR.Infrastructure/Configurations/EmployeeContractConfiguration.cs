using EXE201_BE_ThrivoHR.Domain.Entities.Contracts;

namespace EXE201_BE_ThrivoHR.Infrastructure.Configurations;

public class EmployeeContractConfiguration : IEntityTypeConfiguration<EmployeeContract>
{
    public void Configure(EntityTypeBuilder<EmployeeContract> builder)
    {
        builder.Property(x => x.Salary).HasColumnType("decimal(18,2)");
    }
}
