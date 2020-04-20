using System;
using Aiwins.Rocket.Auditing;
using Aiwins.Rocket.Data;

namespace Aiwins.Rocket.Application.Dtos {
    /// <summary>
    /// 实现了 <see cref="IFullAuditedObject"/> 接口的实体映射对象
    /// 它同时实现了 <see cref="IHasExtraProperties"/> 接口.
    /// </summary>
    /// <typeparam name="TPrimaryKey">主键类型</typeparam>
    [Serializable]
    public abstract class ExtensibleFullAuditedEntityDto<TPrimaryKey> : ExtensibleAuditedEntityDto<TPrimaryKey>, IFullAuditedObject {
        /// <inheritdoc />
        public bool IsDeleted { get; set; }

        /// <inheritdoc />
        public Guid? DeleterId { get; set; }

        /// <inheritdoc />
        public DateTimeOffset? DeletionTime { get; set; }
    }

    /// <summary>
    /// 实现了 <see cref="IFullAuditedObject"/> 接口的实体映射对象
    /// 它同时实现了 <see cref="IHasExtraProperties"/> 接口。
    /// </summary>
    [Serializable]
    public abstract class ExtensibleFullAuditedEntityDto : ExtensibleAuditedEntityDto, IFullAuditedObject {
        /// <inheritdoc />
        public bool IsDeleted { get; set; }

        /// <inheritdoc />
        public Guid? DeleterId { get; set; }

        /// <inheritdoc />
        public DateTimeOffset? DeletionTime { get; set; }
    }
}