using System.Collections.Generic;

namespace Aiwins.Rocket.Application.Dtos {
    /// <summary>
    /// 结果集合
    /// </summary>
    /// <typeparam name="T">集合的数据类型 <see cref="Items"/></typeparam>
    public interface IListResult<T> {
        /// <summary>
        /// 集合
        /// </summary>
        IReadOnlyList<T> Items { get; set; }
    }
}