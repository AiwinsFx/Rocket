using System;
using System.Collections.Generic;

namespace Aiwins.Rocket.Application.Dtos {
    /// <summary>
    /// 实现接口 <see cref="IPagedResult{T}"/>。
    /// </summary>
    /// <typeparam name="T">集合的数据类型 <see cref="ListResultDto{T}.Items"/></typeparam>
    [Serializable]
    public class PagedResultDto<T> : ListResultDto<T>, IPagedResult<T> {
        /// <inheritdoc />
        public long TotalCount { get; set; } //TODO: 考虑数据量的大小问题?

        /// <summary>
        /// 创建一个新的 <see cref="PagedResultDto{T}"/> 对象。
        /// </summary>
        public PagedResultDto () {

        }

        /// <summary>
        /// 创建一个新的 <see cref="PagedResultDto{T}"/> 对象。
        /// </summary>
        /// <param name="totalCount">数据总数</param>
        /// <param name="items">当前页的数据</param>
        public PagedResultDto (long totalCount, IReadOnlyList<T> items) : base (items) {
            TotalCount = totalCount;
        }
    }
}