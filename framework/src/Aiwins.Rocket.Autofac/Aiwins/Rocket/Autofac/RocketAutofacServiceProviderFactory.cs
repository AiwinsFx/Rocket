using System;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;

namespace Aiwins.Rocket.Autofac {
    /// <summary>
    /// 服务工厂 <see cref="T:Autofac.ContainerBuilder" /> 和 <see cref="T:System.IServiceProvider" />
    /// </summary>
    public class RocketAutofacServiceProviderFactory : IServiceProviderFactory<ContainerBuilder> {
        private readonly ContainerBuilder _builder;
        private IServiceCollection _services;

        public RocketAutofacServiceProviderFactory (ContainerBuilder builder) {
            _builder = builder;
        }

        /// <summary>
        /// 从服务集合 <see cref="T:Microsoft.Extensions.DependencyInjection.IServiceCollection" /> 创建一个服务容器构建者
        /// </summary>
        /// <param name="services">服务集合</param>
        /// <returns>返回服务提供者 <see cref="T:System.IServiceProvider" />.</returns>
        public ContainerBuilder CreateBuilder (IServiceCollection services) {
            _services = services;

            _builder.Populate (services);

            return _builder;
        }

        public IServiceProvider CreateServiceProvider (ContainerBuilder containerBuilder) {
            Check.NotNull (containerBuilder, nameof (containerBuilder));

            return new AutofacServiceProvider (containerBuilder.Build ());
        }
    }
}