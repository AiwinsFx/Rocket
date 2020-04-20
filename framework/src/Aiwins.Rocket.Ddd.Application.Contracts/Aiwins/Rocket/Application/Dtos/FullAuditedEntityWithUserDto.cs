using System;
using Aiwins.Rocket.Auditing;

namespace Aiwins.Rocket.Application.Dtos {
    /// <summary>
    /// 实现了 <see cref="IFullAuditedObject{TUser}"/> 接口的实体映射对象
    /// 它包含 <see cref="Creator"/>, <see cref="LastModifier"/> 和 <see cref="Deleter"/> 等审计属性
    /// </summary>
    /// <typeparam name="TUserDto">用户映射实体类型</typeparam>
    [Serializable]
    public abstract class FullAuditedEntityWithUserDto<TUserDto> : FullAuditedEntityDto, IFullAuditedObject<TUserDto> {
        /// <inheritdoc />
        public TUserDto Creator { get; set; }

        /// <inheritdoc />
        public TUserDto LastModifier { get; set; }

        /// <inheritdoc />
        public TUserDto Deleter { get; set; }
    }

    /// <summary>
    /// 实现了 <see cref="IFullAuditedObject{TUser}"/> 接口的实体映射对象
    /// 它包含 <see cref="Creator"/>, <see cref="LastModifier"/> 和 <see cref="Deleter"/> 等审计属性
    /// </summary>
    /// <typeparam name="TPrimaryKey">主键类型</typeparam>
    /// <typeparam name="TUserDto">用户映射实体类型</typeparam>
    [Serializable]
    public abstract class FullAuditedEntityWithUserDto<TPrimaryKey, TUserDto> : FullAuditedEntityDto<TPrimaryKey>, IFullAuditedObject<TUserDto> {
        /// <inheritdoc />
        public TUserDto Creator { get; set; }

        /// <inheritdoc />
        public TUserDto LastModifier { get; set; }

        /// <inheritdoc />
        public TUserDto Deleter { get; set; }
    }
}