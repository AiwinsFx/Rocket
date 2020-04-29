using System;
using Aiwins.Rocket.Domain.Entities.Auditing;
using Aiwins.Rocket.MultiTenancy;
using JetBrains.Annotations;

namespace Aiwins.Rocket.PermissionManagement {
    public class PermissionGrant : Entity<Guid>, IAggregateRoot<Guid>, IMultiTenant {
        public virtual Guid? TenantId { get; protected set; }

        [NotNull]
        public virtual string Name { get; protected set; }

        [NotNull]
        public virtual string ProviderName { get; protected set; }

        [NotNull]
        public virtual string ProviderScope { get; set; }

        [CanBeNull]
        public virtual string ProviderKey { get; protected internal set; }

        protected PermissionGrant () {

        }

        public PermissionGrant (
            Guid id, [NotNull] string name, [NotNull] string providerName, [NotNull] string providerScope, [CanBeNull] string providerKey,
            Guid? tenantId = null) {
            Check.NotNull (name, nameof (name));

            Id = id;
            Name = Check.NotNullOrWhiteSpace (name, nameof (name));
            ProviderName = Check.NotNullOrWhiteSpace (providerName, nameof (providerName));
            ProviderScope = Check.NotNullOrWhiteSpace (providerScope, nameof (providerScope));
            ProviderKey = providerKey;
            TenantId = tenantId;
        }
    }
}