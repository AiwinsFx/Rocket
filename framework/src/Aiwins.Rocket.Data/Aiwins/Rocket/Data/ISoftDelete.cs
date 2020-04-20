namespace Aiwins.Rocket.Data {
    /// <summary>
    /// 用于标识被删除的对象实体
    /// 软删除的的实体并未在数据库中真正的删除,
    /// 而是在数据库中设置属性 IsDeleted = true,
    /// 软删除的实体在数据库中不可见。
    /// </summary>
    public interface ISoftDelete {
        /// <summary>
        /// 对象软删除标识
        /// </summary>
        bool IsDeleted { get; set; }
    }
}