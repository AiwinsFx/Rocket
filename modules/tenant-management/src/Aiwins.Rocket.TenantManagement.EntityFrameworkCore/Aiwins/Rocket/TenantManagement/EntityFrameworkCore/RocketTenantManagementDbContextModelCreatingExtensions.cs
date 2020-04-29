using System;
using Aiwins.Rocket.EntityFrameworkCore.Modeling;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;

namespace Aiwins.Rocket.TenantManagement.EntityFrameworkCore {
    public static class RocketTenantManagementDbContextModelCreatingExtensions {
        public static void ConfigureTenantManagement (
            this ModelBuilder builder, [CanBeNull] Action<RocketTenantManagementModelBuilderConfigurationOptions> optionsAction = null) {
            Check.NotNull (builder, nameof (builder));

            var options = new RocketTenantManagementModelBuilderConfigurationOptions (
                RocketTenantManagementDbProperties.DbTablePrefix,
                RocketTenantManagementDbProperties.DbSchema
            );

            optionsAction?.Invoke (options);

            builder.Entity<Tenant> (b => {
                b.ToTable (options.TablePrefix + "Tenants", options.Schema);

                b.ConfigureByConvention ();

                b.Property (t => t.Name).IsRequired ().HasMaxLength (TenantConsts.MaxNameLength);

                b.Property (t => t.FirstPySpelling).HasMaxLength (TenantConsts.MaxNameLength / 2);

                b.Property (t => t.FullPySpelling).HasMaxLength (TenantConsts.MaxNameLength * 2);

                b.HasMany (u => u.ConnectionStrings).WithOne ().HasForeignKey (uc => uc.TenantId).IsRequired ();

                b.HasIndex (u => u.Name);
            });

            builder.Entity<TenantConnectionString> (b => {
                b.ToTable (options.TablePrefix + "TenantConnectionStrings", options.Schema);

                b.ConfigureByConvention ();

                b.HasKey (x => new { x.TenantId, x.Name });

                b.Property (cs => cs.Name).IsRequired ().HasMaxLength (TenantConnectionStringConsts.MaxNameLength);
                b.Property (cs => cs.Value).IsRequired ().HasMaxLength (TenantConnectionStringConsts.MaxValueLength);
            });
        }
    }
}