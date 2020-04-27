using System;
using Aiwins.Rocket.Auditing;

namespace Aiwins.Rocket.Application.Dtos {
    /// <summary>
    /// 实现了 <see cref="IAuditedObject"/> 接口的实体映射对象
    /// </summary>
    [Serializable]
    public abstract class AuditedEntityDto : CreationAuditedEntityDto, IAuditedObject {
        /// <inheritdoc />
        public DateTime? LastModificationTime { get; set; }

        /// <inheritdoc />
        public Guid? LastModifierId { get; set; }
    }

    /// <summary>
    /// 实现了 <see cref="IAuditedObject"/> 接口的实体映射对象
    /// </summary>
    /// <typeparam name="TPrimaryKey">主键类型</typeparam>
    [Serializable]
    public abstract class AuditedEntityDto<TPrimaryKey> : CreationAuditedEntityDto<TPrimaryKey>, IAuditedObject {
        /// <inheritdoc />
        public DateTime? LastModificationTime { get; set; }

        /// <inheritdoc />
        public Guid? LastModifierId { get; set; }
    }
}