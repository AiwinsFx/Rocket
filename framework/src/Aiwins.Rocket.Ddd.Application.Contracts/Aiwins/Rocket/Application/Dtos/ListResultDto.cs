using System;
using System.Collections.Generic;

namespace Aiwins.Rocket.Application.Dtos {
    [Serializable]
    public class ListResultDto<T> : IListResult<T> {
        /// <inheritdoc />
        public IReadOnlyList<T> Items {
            get { return _items ?? (_items = new List<T> ()); }
            set { _items = value; }
        }
        private IReadOnlyList<T> _items;

        /// <summary>
        /// 创建一个新的 <see cref="ListResultDto{T}"/> 对象。
        /// </summary>
        public ListResultDto () {

        }

        /// <summary>
        /// 创建一个新的 <see cref="ListResultDto{T}"/> 对象。
        /// </summary>
        /// <param name="items">List of items</param>
        public ListResultDto (IReadOnlyList<T> items) {
            Items = items;
        }
    }
}