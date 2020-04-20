namespace Aiwins.Rocket.EventBus {
    /// <summary>
    /// 此接口可以被以下的事件数据类实现
    /// 比如有一个单一的泛型参数，这个参数将被继承使用。
    ///
    /// 例如；
    /// 假设Student类继承自Person，拥有EntityCreatedEventData{Student}事件数据类
    /// EntityCreatedEventData可以实现这个接口。
    /// </summary>
    public interface IEventDataWithInheritableGenericArgument {
        /// <summary>
        /// 获取创建实例对象时的参数
        /// </summary>
        /// <returns>构造函数参数</returns>
        object[] GetConstructorArgs ();
    }
}