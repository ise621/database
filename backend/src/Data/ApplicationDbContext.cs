using Database.Enumerations;
using Microsoft.AspNetCore.DataProtection.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchemaNameOptionsExtension = Database.Data.Extensions.SchemaNameOptionsExtension;

namespace Database.Data;

// Inspired by
// [Authentication and authorization for SPAs](https://docs.microsoft.com/en-us/aspnet/core/security/authentication/identity-api-authorization?view=aspnetcore-3.0)
// [Customize Identity Model](https://docs.microsoft.com/en-us/aspnet/core/security/authentication/customize-identity-model?view=aspnetcore-3.0)
public sealed class ApplicationDbContext
    : DbContext, IDataProtectionKeyContext
{
    private readonly string _schemaName;

    public ApplicationDbContext(
        DbContextOptions<ApplicationDbContext> options
    )
        : base(options)
    {
        var schemaNameOptions = options.FindExtension<SchemaNameOptionsExtension>();
        _schemaName = schemaNameOptions is null ? "database" : schemaNameOptions.SchemaName;
    }

    // https://docs.microsoft.com/en-us/ef/core/miscellaneous/nullable-reference-types#dbcontext-and-dbset
    public DbSet<GetHttpsResource> GetHttpsResources { get; private set; } = default!;
    public DbSet<CalorimetricData> CalorimetricData { get; private set; } = default!;
    public DbSet<HygrothermalData> HygrothermalData { get; private set; } = default!;
    public DbSet<OpticalData> OpticalData { get; private set; } = default!;
    public DbSet<PhotovoltaicData> PhotovoltaicData { get; private set; } = default!;
    public DbSet<User> Users { get; private set; } = default!;
    public DbSet<DataProtectionKey> DataProtectionKeys { get; private set; } = default!;

    private static void CreateEnumerations(ModelBuilder builder)
    {
        // https://www.npgsql.org/efcore/mapping/enum.html#creating-your-database-enum
        // Create enumerations in public schema because that is where
        // `NpgsqlDataSourceBuilder.MapEnum` expects them to be by default.
        builder.HasPostgresEnum<DataKind>("public");
        builder.HasPostgresEnum<Enumerations.Standardizer>("public");
    }

    private static
        EntityTypeBuilder<TEntity>
        ConfigureEntity<TEntity>(
            EntityTypeBuilder<TEntity> builder
        )
        where TEntity : Entity
    {
        // https://www.npgsql.org/efcore/modeling/generated-properties.html#guiduuid-generation
        builder
            .Property(e => e.Id)
            .HasDefaultValueSql("gen_random_uuid()");
        // https://www.npgsql.org/efcore/modeling/concurrency.html#the-postgresql-xmin-system-column
        builder
            .Property(e => e.Version)
            .IsRowVersion();
        return builder;
    }

    private static void ConfigureIdentityEntities(
        ModelBuilder builder
    )
    {
        // https://stackoverflow.com/questions/19902756/asp-net-identity-dbcontext-confusion/35722688#35722688
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.HasDefaultSchema(_schemaName);
        modelBuilder.HasPostgresExtension(
            "pgcrypto"); // https://www.npgsql.org/efcore/modeling/generated-properties.html#guiduuid-generation
        CreateEnumerations(modelBuilder);
        ConfigureIdentityEntities(modelBuilder);
        ConfigureEntity(
                modelBuilder.Entity<GetHttpsResource>()
            )
            .ToTable("get_https_resource");
        ConfigureEntity(
                modelBuilder.Entity<CalorimetricData>()
            )
            .OwnsOne(
                data => data.AppliedMethod,
                method =>
                {
                    method.OwnsMany(m => m.Arguments);
                    method.OwnsMany(m => m.Sources);
                }
            )
            .ToTable("calorimetric_data");
        ConfigureEntity(
                modelBuilder.Entity<HygrothermalData>()
            )
            .ToTable("hygrothermal_data");
        ConfigureEntity(
                modelBuilder.Entity<OpticalData>()
            )
            .ToTable("optical_data");
        ConfigureEntity(
                modelBuilder.Entity<PhotovoltaicData>()
            )
            .ToTable("photovoltaic_data");
        ConfigureEntity(
                modelBuilder.Entity<User>()
            )
            .ToTable("user");
    }
}