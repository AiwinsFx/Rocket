namespace Aiwins.Rocket.Auditing {
    /// <summary>
    /// 审计对象接口
    /// </summary>
    public interface IAuditedObject : ICreationAuditedObject, IModificationAuditedObject {

    }

    /// <summary>
    /// 审计对象 <see cref="IAuditedObject"/> 接口
    /// </summary>
    /// <typeparam name="TUser">用户类型</typeparam>
    public interface IAuditedObject<TUser> : IAuditedObject, ICreationAuditedObject<TUser>, IModificationAuditedObject<TUser> {

    }
}