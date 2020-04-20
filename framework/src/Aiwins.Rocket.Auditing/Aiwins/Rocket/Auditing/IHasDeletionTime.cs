using System;
using Aiwins.Rocket.Data;

namespace Aiwins.Rocket.Auditing {
    /// <summary>
    /// 审计对象删除时间接口
    /// 软删除 (see <see cref="ISoftDelete"/>).
    /// </summary>
    public interface IHasDeletionTime : ISoftDelete {
        /// <summary>
        /// 删除时间
        /// </summary>
        DateTimeOffset? DeletionTime { get; set; }
    }
}