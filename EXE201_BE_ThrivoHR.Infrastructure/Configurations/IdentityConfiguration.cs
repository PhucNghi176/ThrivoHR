using EXE201_BE_ThrivoHR.Infrastructure.Constants;
using Microsoft.AspNetCore.Identity;

namespace EXE201_BE_ThrivoHR.Infrastructure.Configurations;

internal sealed class AppUserRoleConfiguration : IEntityTypeConfiguration<IdentityUserRole<string>>
{
    public void Configure(EntityTypeBuilder<IdentityUserRole<string>> builder)
    {
        builder.ToTable(TableNames.AppUserRoles);

        builder.HasKey(x => new { x.RoleId, x.UserId });
    }
}

internal sealed class AppRoleClaimConfiguration : IEntityTypeConfiguration<IdentityRoleClaim<string>>
{
    public void Configure(EntityTypeBuilder<IdentityRoleClaim<string>> builder)
    {
        builder.ToTable(TableNames.AppRoleClaims);

        builder.HasKey(x => x.RoleId);
    }
}

internal sealed class AppUserClaimConfiguration : IEntityTypeConfiguration<IdentityUserClaim<string>>
{
    public void Configure(EntityTypeBuilder<IdentityUserClaim<string>> builder)
    {
        builder.ToTable(TableNames.AppUserClaims);

        builder.HasKey(x => x.UserId);
    }
}

internal sealed class AppUserLoginConfiguration : IEntityTypeConfiguration<IdentityUserLogin<string>>
{
    public void Configure(EntityTypeBuilder<IdentityUserLogin<string>> builder)
    {
        builder.ToTable(TableNames.AppUserLogins);

        builder.HasKey(x => x.UserId);
    }
}

internal sealed class AppUserTokenConfiguration : IEntityTypeConfiguration<IdentityUserToken<string>>
{
    public void Configure(EntityTypeBuilder<IdentityUserToken<string>> builder)
    {
        builder.ToTable(TableNames.AppUserTokens);

        builder.HasKey(x => x.UserId);
    }
}
