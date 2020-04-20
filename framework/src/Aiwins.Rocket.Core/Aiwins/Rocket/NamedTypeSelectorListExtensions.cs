using System;
using System.Collections.Generic;
using System.Linq;

namespace Aiwins.Rocket {
    public static class NamedTypeSelectorListExtensions {
        /// <summary>
        /// NamedTypeSelector集合添加子项
        /// </summary>
        /// <param name="list">NamedTypeSelector集合</param>
        /// <param name="name">选择器唯一名称，可用于从列表中删除指定类型</param>
        /// <param name="types"></param>
        public static void Add (this IList<NamedTypeSelector> list, string name, params Type[] types) {
            Check.NotNull (list, nameof (list));
            Check.NotNull (name, nameof (name));
            Check.NotNull (types, nameof (types));

            list.Add (new NamedTypeSelector (name, type => types.Any (type.IsAssignableFrom)));
        }
    }
}