using System;
using System.Collections.Generic;
using Aiwins.Rocket.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Aiwins.Rocket.Data {
    public class DefaultConnectionStringResolver : IConnectionStringResolver, ITransientDependency {
        protected RocketDbConnectionOptions Options { get; }

        public DefaultConnectionStringResolver (IOptionsSnapshot<RocketDbConnectionOptions> options) {
            Options = options.Value;
        }

        public virtual string Resolve (string connectionStringName = null) {
            //优先获取业务模块提供的值
            if (!connectionStringName.IsNullOrEmpty ()) {
                var moduleConnString = Options.ConnectionStrings.GetOrDefault (connectionStringName);
                if (!moduleConnString.IsNullOrEmpty ()) {
                    return moduleConnString;
                }
            }

            //如果业务模块未提供，则获取默认值
            return Options.ConnectionStrings.Default;
        }
    }
}