using System;
using Aiwins.Rocket.Auditing;
using Aiwins.Rocket.Domain.Entities;
using Aiwins.Rocket.Guids;
using Aiwins.Rocket.MultiTenancy;

namespace Aiwins.Rocket.AuditLogging {
    [DisableAuditing]
    public class EntityPropertyChange : Entity<Guid>, IMultiTenant {
        public virtual Guid? TenantId { get; protected set; }

        public virtual Guid EntityChangeId { get; protected set; }

        public virtual string NewValue { get; protected set; }

        public virtual string OriginalValue { get; protected set; }

        public virtual string PropertyName { get; protected set; }

        public virtual string PropertyTypeFullName { get; protected set; }

        protected EntityPropertyChange () {

        }

        public EntityPropertyChange (
            IGuidGenerator guidGenerator,
            Guid entityChangeId,
            EntityPropertyChangeInfo entityChangeInfo,
            Guid? tenantId = null) {
            Id = guidGenerator.Create ();
            TenantId = tenantId;
            EntityChangeId = entityChangeId;
            NewValue = entityChangeInfo.NewValue.Truncate (EntityPropertyChangeConsts.MaxNewValueLength);
            OriginalValue = entityChangeInfo.OriginalValue.Truncate (EntityPropertyChangeConsts.MaxOriginalValueLength);
            PropertyName = entityChangeInfo.PropertyName.TruncateFromBeginning (EntityPropertyChangeConsts.MaxPropertyNameLength);
            PropertyTypeFullName = entityChangeInfo.PropertyTypeFullName.TruncateFromBeginning (EntityPropertyChangeConsts.MaxPropertyTypeFullNameLength);
        }
    }
}