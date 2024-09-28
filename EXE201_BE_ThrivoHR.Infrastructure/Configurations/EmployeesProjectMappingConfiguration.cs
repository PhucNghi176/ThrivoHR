using EXE201_BE_ThrivoHR.Domain.Entities;

namespace EXE201_BE_ThrivoHR.Infrastructure.Configurations;

internal class EmployeesProjectMappingConfiguration : IEntityTypeConfiguration<EmployeesProjectMapping>
{
    public void Configure(EntityTypeBuilder<EmployeesProjectMapping> builder)
    {
        builder.HasKey(e => new { e.EmployeeId, e.ProjectId });
    }
}
