using System;
using Aiwins.Rocket.Domain.Entities;
using JetBrains.Annotations;

namespace Aiwins.Rocket.TenantManagement {
    public class TenantConnectionString : Entity {
        public virtual Guid TenantId { get; protected set; }

        public virtual string Name { get; protected set; }

        public virtual string Value { get; protected set; }

        protected TenantConnectionString () {

        }

        public TenantConnectionString (Guid tenantId, [NotNull] string name, [NotNull] string value) {
            TenantId = tenantId;
            Name = Check.NotNullOrWhiteSpace (name, nameof (name), TenantConnectionStringConsts.MaxNameLength);
            SetValue (value);
        }

        public virtual void SetValue ([NotNull] string value) {
            Value = Check.NotNullOrWhiteSpace (value, nameof (value), TenantConnectionStringConsts.MaxValueLength);
        }

        public override object[] GetKeys () {
            return new object[] { TenantId, Name };
        }
    }
}