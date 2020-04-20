using System;
using Aiwins.Rocket.Auditing;
using Aiwins.Rocket.Data;

namespace Aiwins.Rocket.Application.Dtos {
    /// <summary>
    /// 实现了 <see cref="IAuditedObject"/> 接口的实体映射对象
    /// 拥有 <see cref="Creator"/> 和 <see cref="LastModifier"/> 等审计对象
    /// 它同时实现了 <see cref="IHasExtraProperties"/> 接口。
    /// </summary>
    /// <typeparam name="TUserDto">用户映射实体类型</typeparam>
    [Serializable]
    public abstract class ExtensibleAuditedEntityWithUserDto<TUserDto> : ExtensibleAuditedEntityDto,
        IAuditedObject<TUserDto> {
            /// <inheritdoc />
            public TUserDto Creator { get; set; }

            /// <inheritdoc />
            public TUserDto LastModifier { get; set; }
        }

    /// <summary>
    /// 实现了 <see cref="IAuditedObject"/> 接口的实体映射对象
    /// 拥有 <see cref="Creator"/> 和 <see cref="LastModifier"/> 等审计对象
    /// 它同时实现了 <see cref="IHasExtraProperties"/> 接口。
    /// </summary>
    /// <typeparam name="TPrimaryKey">主键类型</typeparam>
    /// <typeparam name="TUserDto">用户映射实体类型</typeparam>
    [Serializable]
    public abstract class ExtensibleAuditedEntityWithUserDto<TPrimaryKey, TUserDto> : ExtensibleAuditedEntityDto<TPrimaryKey>, IAuditedObject<TUserDto> {
        /// <inheritdoc />
        public TUserDto Creator { get; set; }

        /// <inheritdoc />
        public TUserDto LastModifier { get; set; }
    }
}