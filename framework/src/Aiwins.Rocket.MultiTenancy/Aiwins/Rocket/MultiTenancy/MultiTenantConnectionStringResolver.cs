using System;
using System.Collections.Generic;
using Aiwins.Rocket.Data;
using Aiwins.Rocket.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Aiwins.Rocket.MultiTenancy {
    [Dependency (ReplaceServices = true)]
    public class MultiTenantConnectionStringResolver : DefaultConnectionStringResolver {
        private readonly ICurrentTenant _currentTenant;
        private readonly IServiceProvider _serviceProvider;

        public MultiTenantConnectionStringResolver (
            IOptionsSnapshot<RocketDbConnectionOptions> options,
            ICurrentTenant currentTenant,
            IServiceProvider serviceProvider) : base (options) {
            _currentTenant = currentTenant;
            _serviceProvider = serviceProvider;
        }

        public override string Resolve (string connectionStringName = null) {
            // 租户信息为空，则执行默认字符串链接解析逻辑
            if (_currentTenant.Id == null) {
                return base.Resolve (connectionStringName);
            }

            using (var serviceScope = _serviceProvider.CreateScope ()) {
                var tenantStore = serviceScope
                    .ServiceProvider
                    .GetRequiredService<ITenantStore> ();

                var tenant = tenantStore.Find (_currentTenant.Id.Value);

                if (tenant?.ConnectionStrings == null) {
                    return base.Resolve (connectionStringName);
                }

                // 获取默认字符串链接
                if (connectionStringName == null) {
                    return tenant.ConnectionStrings.Default ??
                        Options.ConnectionStrings.Default;
                }

                // 获取租户的字符串链接
                var connString = tenant.ConnectionStrings.GetOrDefault (connectionStringName);
                if (connString != null) {
                    return connString;
                }

                // 如果租户字符串链接不存在，则尝试获取配置的字符串链接

                var connStringInOptions = Options.ConnectionStrings.GetOrDefault (connectionStringName);
                if (connStringInOptions != null) {
                    return connStringInOptions;
                }

                return tenant.ConnectionStrings.Default ??
                    Options.ConnectionStrings.Default;
            }
        }
    }
}