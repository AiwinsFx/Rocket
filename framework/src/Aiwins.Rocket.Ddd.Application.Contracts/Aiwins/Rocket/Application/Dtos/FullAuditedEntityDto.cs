using System;
using Aiwins.Rocket.Auditing;

namespace Aiwins.Rocket.Application.Dtos {
    /// <summary>
    /// 实现了 <see cref="IFullAuditedObject"/> 接口的实体映射对象
    /// </summary>
    [Serializable]
    public abstract class FullAuditedEntityDto : AuditedEntityDto, IFullAuditedObject {
        /// <inheritdoc />
        public bool IsDeleted { get; set; }

        /// <inheritdoc />
        public Guid? DeleterId { get; set; }

        /// <inheritdoc />
        public DateTime? DeletionTime { get; set; }
    }

    /// <summary>
    /// 实现了 <see cref="IFullAuditedObject"/> 接口的实体映射对象
    /// </summary>
    /// <typeparam name="TPrimaryKey">主键类型</typeparam>
    [Serializable]
    public abstract class FullAuditedEntityDto<TPrimaryKey> : AuditedEntityDto<TPrimaryKey>, IFullAuditedObject {
        /// <inheritdoc />
        public bool IsDeleted { get; set; }

        /// <inheritdoc />
        public Guid? DeleterId { get; set; }

        /// <inheritdoc />
        public DateTime? DeletionTime { get; set; }
    }
}