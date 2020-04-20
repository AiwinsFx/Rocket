namespace Aiwins.Rocket.Auditing {
    /// <summary>
    /// 富审计对象
    /// </summary>
    public interface IFullAuditedObject : IAuditedObject, IDeletionAuditedObject {

    }

    /// <summary>
    /// 富审计对象 <see cref="IFullAuditedObject"/> 接口
    /// </summary>
    /// <typeparam name="TUser">用户类型</typeparam>
    public interface IFullAuditedObject<TUser> : IAuditedObject<TUser>, IFullAuditedObject, IDeletionAuditedObject<TUser> {

    }
}