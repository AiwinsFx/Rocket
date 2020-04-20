using System;

namespace Aiwins.Rocket.Auditing {
    /// <summary>
    /// 审计对象最近更新时间接口
    /// </summary>
    public interface IHasModificationTime {
        /// <summary>
        /// 最近更新时间
        /// </summary>
        DateTimeOffset? LastModificationTime { get; set; }
    }
}