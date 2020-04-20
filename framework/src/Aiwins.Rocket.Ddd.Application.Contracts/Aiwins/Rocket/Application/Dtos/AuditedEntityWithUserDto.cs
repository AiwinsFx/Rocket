using System;
using Aiwins.Rocket.Auditing;

namespace Aiwins.Rocket.Application.Dtos {
    /// <summary>
    /// 实现了 <see cref="IAuditedObject"/> 接口的实体映射对象
    /// 拥有 <see cref="Creator"/> 和 <see cref="LastModifier"/> 等审计对象
    /// </summary>
    /// <typeparam name="TUserDto">用户映射实体类型</typeparam>
    [Serializable]
    public abstract class AuditedEntityWithUserDto<TUserDto> : AuditedEntityDto, IAuditedObject<TUserDto> {
        /// <inheritdoc />
        public TUserDto Creator { get; set; }

        /// <inheritdoc />
        public TUserDto LastModifier { get; set; }
    }

    /// <summary>
    /// 实现了 <see cref="IAuditedObject"/> 接口的实体映射对象
    /// 拥有 <see cref="Creator"/> 和 <see cref="LastModifier"/> 等审计对象
    /// </summary>
    /// <typeparam name="TPrimaryKey">主键类型</typeparam>
    /// <typeparam name="TUserDto">用户映射实体类型</typeparam>
    [Serializable]
    public abstract class AuditedEntityWithUserDto<TPrimaryKey, TUserDto> : AuditedEntityDto<TPrimaryKey>, IAuditedObject<TUserDto> {
        /// <inheritdoc />
        public TUserDto Creator { get; set; }

        /// <inheritdoc />
        public TUserDto LastModifier { get; set; }
    }
}