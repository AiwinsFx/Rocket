using System;
using Aiwins.Rocket.Auditing;
using Aiwins.Rocket.Data;

namespace Aiwins.Rocket.Application.Dtos {
    /// <summary>
    /// 实现了 <see cref="IFullAuditedObject{TUser}"/> 接口的实体映射对象
    /// 它包含 <see cref="Creator"/>, <see cref="LastModifier"/> 和 <see cref="Deleter"/> 等审计对象
    /// 它同时实现了 <see cref="IHasExtraProperties"/> 接口.
    /// </summary>
    /// <typeparam name="TUserDto">用户映射实体类型</typeparam>
    [Serializable]
    public abstract class ExtensibleFullAuditedEntityWithUserDto<TUserDto> : ExtensibleFullAuditedEntityDto,
        IFullAuditedObject<TUserDto> {
            /// <inheritdoc />
            public TUserDto Creator { get; set; }

            /// <inheritdoc />
            public TUserDto LastModifier { get; set; }

            /// <inheritdoc />
            public TUserDto Deleter { get; set; }
        }

    /// <summary>
    /// 实现了 <see cref="IFullAuditedObject{TUser}"/> 接口的实体映射对象
    /// 它包含 <see cref="Creator"/>, <see cref="LastModifier"/> 和 <see cref="Deleter"/> 等审计对象
    /// 它同时实现了 <see cref="IHasExtraProperties"/> 接口.
    /// </summary>
    /// <typeparam name="TPrimaryKey">主键类型</typeparam>
    /// <typeparam name="TUserDto">用户映射实体类型</typeparam>
    [Serializable]
    public abstract class ExtensibleFullAuditedEntityWithUserDto<TPrimaryKey, TUserDto> : ExtensibleFullAuditedEntityDto<TPrimaryKey>, IFullAuditedObject<TUserDto> {
        /// <inheritdoc />
        public TUserDto Creator { get; set; }

        /// <inheritdoc />
        public TUserDto LastModifier { get; set; }

        /// <inheritdoc />
        public TUserDto Deleter { get; set; }
    }
}