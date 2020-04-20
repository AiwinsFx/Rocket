using System;
using Aiwins.Rocket.Auditing;

namespace Aiwins.Rocket.Application.Dtos
{
    /// <summary>
    /// 实现了 <see cref="ICreationAuditedObject{TCreator}"/> 接口的实体映射对象
    /// 拥有 <see cref="Creator"/> 审计对象
    /// </summary>
    /// <typeparam name="TUserDto">用户映射实体类型</typeparam>
    [Serializable]
    public abstract class CreationAuditedEntityWithUserDto<TUserDto> : CreationAuditedEntityDto, ICreationAuditedObject<TUserDto>
    {
        public TUserDto Creator { get; set; }
    }

    /// <summary>
    /// 实现了 <see cref="ICreationAuditedObject{TCreator}"/> 接口的实体映射对象
    /// 拥有 <see cref="Creator"/> 审计对象
    /// </summary>
    /// <typeparam name="TPrimaryKey">主键类型</typeparam>
    /// <typeparam name="TUserDto">用户映射实体类型</typeparam>
    [Serializable]
    public abstract class CreationAuditedEntityWithUserDto<TPrimaryKey, TUserDto> : CreationAuditedEntityDto<TPrimaryKey>, ICreationAuditedObject<TUserDto>
    {
        public TUserDto Creator { get; set; }
    }
}