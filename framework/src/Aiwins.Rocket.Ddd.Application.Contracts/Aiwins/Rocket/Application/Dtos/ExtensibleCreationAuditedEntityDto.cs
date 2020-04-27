using System;
using Aiwins.Rocket.Auditing;
using Aiwins.Rocket.Data;

namespace Aiwins.Rocket.Application.Dtos {
    /// <summary>
    /// 实现了 <see cref="ICreationAuditedObject"/> 接口的实体映射对象
    /// 它同时实现了 <see cref="IHasExtraProperties"/> 接口。
    /// </summary>
    /// <typeparam name="TPrimaryKey">主键类型</typeparam>
    [Serializable]
    public abstract class ExtensibleCreationAuditedEntityDto<TPrimaryKey> : ExtensibleEntityDto<TPrimaryKey>, ICreationAuditedObject {
        /// <inheritdoc />
        public DateTime CreationTime { get; set; }

        /// <inheritdoc />
        public Guid? CreatorId { get; set; }
    }

    /// <summary>
    /// 实现了 <see cref="ICreationAuditedObject"/> 接口的实体映射对象
    /// 它同时实现了 <see cref="IHasExtraProperties"/> 接口。
    /// </summary>
    [Serializable]
    public abstract class ExtensibleCreationAuditedEntityDto : ExtensibleEntityDto, ICreationAuditedObject {
        /// <inheritdoc />
        public DateTime CreationTime { get; set; }

        /// <inheritdoc />
        public Guid? CreatorId { get; set; }
    }
}