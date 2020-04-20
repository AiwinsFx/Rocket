namespace Aiwins.Rocket.Auditing {
    /// <summary>
    /// 审计对象创建者标识接口
    /// </summary>
    public interface ICreationAuditedObject : IHasCreationTime, IMayHaveCreator {

    }

    /// <summary>
    /// 审计对象创建者 <see cref="ICreationAuditedObject"/> 接口
    /// </summary>
    /// <typeparam name="TCreator">用户类型</typeparam>
    public interface ICreationAuditedObject<TCreator> : ICreationAuditedObject, IMayHaveCreator<TCreator> {

    }
}