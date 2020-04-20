using System;

namespace Aiwins.Rocket.Auditing {
    /// <summary>
    /// 审计对象删除标识接口
    /// </summary>
    public interface IDeletionAuditedObject : IHasDeletionTime {
        /// <summary>
        /// 删除者标识
        /// </summary>
        Guid? DeleterId { get; set; }
    }

    /// <summary>
    /// 审计对象删除者的 <see cref="IDeletionAuditedObject"/> 接口
    /// </summary>
    /// <typeparam name="TUser">用户类型</typeparam>
    public interface IDeletionAuditedObject<TUser> : IDeletionAuditedObject {
        /// <summary>
        /// 删除者
        /// </summary>
        TUser Deleter { get; set; }
    }
}