using System.Collections.Generic;
using JetBrains.Annotations;
using Microsoft.Extensions.DependencyInjection;

namespace Aiwins.Rocket.Modularity {
    public class ServiceConfigurationContext {
        public IServiceCollection Services { get; }

        public IDictionary<string, object> Items { get; }

        /// <summary>
        /// 获取和设置可以在各个模块共享的索引器。
        /// 字典集合 <see cref="Items"/>。
        /// 如果字典集合 <see cref="Items"/> 中不存在则返回空值。
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public object this [string key] {
            get => Items.GetOrDefault (key);
            set => Items[key] = value;
        }

        public ServiceConfigurationContext ([NotNull] IServiceCollection services) {
            Services = Check.NotNull (services, nameof (services));
            Items = new Dictionary<string, object> ();
        }
    }
}