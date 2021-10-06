using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Npgsql;
using SchemaNameOptionsExtension = Database.Data.Extensions.SchemaNameOptionsExtension;

namespace Database.Data
{
    // Inspired by
    // [Authentication and authorization for SPAs](https://docs.microsoft.com/en-us/aspnet/core/security/authentication/identity-api-authorization?view=aspnetcore-3.0)
    // [Customize Identity Model](https://docs.microsoft.com/en-us/aspnet/core/security/authentication/customize-identity-model?view=aspnetcore-3.0)
    [Owned]
    public sealed class ApplicationDbContext
          : DbContext
    {
        static ApplicationDbContext()
        {
            RegisterEnumerations();
        }

        private static void RegisterEnumerations()
        {
            // https://www.npgsql.org/efcore/mapping/enum.html#mapping-your-enum
            NpgsqlConnection.GlobalTypeMapper.MapEnum<Enumerations.DataKind>();
        }

        private static void CreateEnumerations(ModelBuilder builder)
        {
            // https://www.npgsql.org/efcore/mapping/enum.html#creating-your-database-enum
            // Create enumerations in public schema because that is where `NpgsqlConnection.GlobalTypeMapper.MapEnum` expects them to be by default.
            builder.HasPostgresEnum<Enumerations.DataKind>("public");
        }

        private static
          EntityTypeBuilder<TEntity>
          ConfigureEntity<TEntity>(
            EntityTypeBuilder<TEntity> builder
            )
          where TEntity : Data.Entity
        {
            // https://www.npgsql.org/efcore/modeling/generated-properties.html#guiduuid-generation
            builder
              .Property(e => e.Id)
              .HasDefaultValueSql("gen_random_uuid()");
            // https://www.npgsql.org/efcore/modeling/concurrency.html#the-postgresql-xmin-system-column
            builder
              .UseXminAsConcurrencyToken();
            return builder;
        }

        private static void ConfigureIdentityEntities(
            ModelBuilder builder
            )
        {
            // https://stackoverflow.com/questions/19902756/asp-net-identity-dbcontext-confusion/35722688#35722688
        }

        private readonly string _schemaName;

        // https://docs.microsoft.com/en-us/ef/core/miscellaneous/nullable-reference-types#dbcontext-and-dbset
        /* public DbSet<Component> Components { get; private set; } = default!; */

        public ApplicationDbContext(
            DbContextOptions<ApplicationDbContext> options
            )
          : base(options)
        {
            var schemaNameOptions = options.FindExtension<SchemaNameOptionsExtension>();
            _schemaName = schemaNameOptions is null ? "database" : schemaNameOptions.SchemaName;
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.HasDefaultSchema(_schemaName);
            builder.HasPostgresExtension("pgcrypto"); // https://www.npgsql.org/efcore/modeling/generated-properties.html#guiduuid-generation
            CreateEnumerations(builder);
            ConfigureIdentityEntities(builder);
            ConfigureEntity(
                builder.Entity<GetHttpsResource>()
                )
              .ToTable("get_https_resource");
            ConfigureEntity(
                builder.Entity<OpticalData>()
                )
              .ToTable("optical_data");
        }
    }
}