using System;

namespace Aiwins.Rocket.Auditing {
    /// <summary>
    /// 审计对象创建时间接口
    /// </summary>
    public interface IHasCreationTime {
        /// <summary>
        /// 创建时间
        /// </summary>
        DateTimeOffset CreationTime { get; set; }
    }
}