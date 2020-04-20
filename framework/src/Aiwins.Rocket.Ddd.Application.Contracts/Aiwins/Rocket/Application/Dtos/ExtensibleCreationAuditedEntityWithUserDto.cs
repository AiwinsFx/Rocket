using System;
using Aiwins.Rocket.Auditing;
using Aiwins.Rocket.Data;

namespace Aiwins.Rocket.Application.Dtos
{
    /// <summary>
    /// 实现了 <see cref="ICreationAuditedObject{TCreator}"/> 接口的实体映射对象
    /// 拥有 <see cref="Creator"/> 审计对象
    /// 它同时实现了 <see cref="IHasExtraProperties"/> 接口。
    /// </summary>
    /// <typeparam name="TPrimaryKey">主键类型</typeparam>
    /// <typeparam name="TUserDto">用户映射实体类型</typeparam>
    [Serializable]
    public abstract class ExtensibleCreationAuditedEntityWithUserDto<TPrimaryKey, TUserDto> : ExtensibleCreationAuditedEntityDto<TPrimaryKey>, ICreationAuditedObject<TUserDto>
    {
        public TUserDto Creator { get; set; }
    }

    /// <summary>
    /// 实现了 <see cref="ICreationAuditedObject{TCreator}"/> 接口的实体映射对象
    /// 拥有 <see cref="Creator"/> 审计对象
    /// 它同时实现了 <see cref="IHasExtraProperties"/> 接口。
    /// </summary>
    /// <typeparam name="TUserDto">用户映射实体类型</typeparam>
    [Serializable]
    public abstract class ExtensibleCreationAuditedEntityWithUserDto<TUserDto> : ExtensibleCreationAuditedEntityDto,
        ICreationAuditedObject<TUserDto>
    {
        public TUserDto Creator { get; set; }
    }
}