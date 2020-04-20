using System;
using Aiwins.Rocket.Auditing;

namespace Aiwins.Rocket.Application.Dtos {
    /// <summary>
    /// 实现了 <see cref="ICreationAuditedObject"/> 接口的实体映射对象
    /// </summary>
    [Serializable]
    public abstract class CreationAuditedEntityDto : EntityDto, ICreationAuditedObject {
        /// <inheritdoc />
        public DateTimeOffset CreationTime { get; set; }

        /// <inheritdoc />
        public Guid? CreatorId { get; set; }
    }

    /// <summary>
    /// 实现了 <see cref="ICreationAuditedObject"/> 接口的实体映射对象
    /// </summary>
    /// <typeparam name="TPrimaryKey">主键类型</typeparam>
    [Serializable]
    public abstract class CreationAuditedEntityDto<TPrimaryKey> : EntityDto<TPrimaryKey>, ICreationAuditedObject {
        /// <inheritdoc />
        public DateTimeOffset CreationTime { get; set; }

        /// <inheritdoc />
        public Guid? CreatorId { get; set; }
    }
}