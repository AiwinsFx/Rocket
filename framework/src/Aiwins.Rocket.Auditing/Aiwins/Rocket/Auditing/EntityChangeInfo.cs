using System;
using System.Collections.Generic;
using System.Linq;
using Aiwins.Rocket.Data;

namespace Aiwins.Rocket.Auditing {
    [Serializable]
    public class EntityChangeInfo : IHasExtraProperties {
        public DateTime ChangeTime { get; set; }

        public EntityChangeType ChangeType { get; set; }

        /// <summary>
        /// 实体的租户标识
        /// </summary>
        public Guid? EntityTenantId { get; set; }

        public string EntityId { get; set; }

        public string EntityTypeFullName { get; set; }

        public List<EntityPropertyChangeInfo> PropertyChanges { get; set; }

        public Dictionary<string, object> ExtraProperties { get; }

        public virtual object EntityEntry { get; set; } //TODO: 考虑删除，它破坏了可序列化性

        public EntityChangeInfo () {
            ExtraProperties = new Dictionary<string, object> ();
        }

        public virtual void Merge (EntityChangeInfo changeInfo) {
            foreach (var propertyChange in changeInfo.PropertyChanges) {
                var existingChange = PropertyChanges.FirstOrDefault (p => p.PropertyName == propertyChange.PropertyName);
                if (existingChange == null) {
                    PropertyChanges.Add (propertyChange);
                } else {
                    existingChange.NewValue = propertyChange.NewValue;
                }
            }

            foreach (var extraProperty in changeInfo.ExtraProperties) {
                var key = extraProperty.Key;
                if (ExtraProperties.ContainsKey (key)) {
                    key = InternalUtils.AddCounter (key);
                }

                ExtraProperties[key] = extraProperty.Value;
            }
        }
    }
}