using System;

namespace Aiwins.Rocket.Auditing {
    /// <summary>
    /// 审计对象最近更新者标识接口
    /// </summary>
    public interface IModificationAuditedObject : IHasModificationTime {
        /// <summary>
        /// 最近更新人标识
        /// </summary>
        Guid? LastModifierId { get; set; }
    }

    /// <summary>
    /// 审计对象最近更新者 <see cref="IModificationAuditedObject"/> 接口
    /// </summary>
    /// <typeparam name="TUser">用户类型</typeparam>
    public interface IModificationAuditedObject<TUser> : IModificationAuditedObject {
        /// <summary>
        /// 最近更新人
        /// </summary>
        TUser LastModifier { get; set; }
    }
}