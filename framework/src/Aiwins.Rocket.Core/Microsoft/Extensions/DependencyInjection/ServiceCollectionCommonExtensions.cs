using System;
using System.Linq;
using System.Reflection;
using Aiwins.Rocket;
using JetBrains.Annotations;

namespace Microsoft.Extensions.DependencyInjection {
    public static class ServiceCollectionCommonExtensions {
        public static bool IsAdded<T> (this IServiceCollection services) {
            return services.IsAdded (typeof (T));
        }

        public static bool IsAdded (this IServiceCollection services, Type type) {
            return services.Any (d => d.ServiceType == type);
        }

        public static T GetSingletonInstanceOrNull<T> (this IServiceCollection services) {
            return (T) services.FirstOrDefault (d => d.ServiceType == typeof (T))?.ImplementationInstance;
        }

        public static T GetSingletonInstance<T> (this IServiceCollection services) {
            var service = services.GetSingletonInstanceOrNull<T> ();
            if (service == null) {
                throw new InvalidOperationException ("Could not find singleton service: " + typeof (T).AssemblyQualifiedName);
            }

            return service;
        }

        public static IServiceProvider BuildServiceProviderFromFactory ([NotNull] this IServiceCollection services) {
            Check.NotNull (services, nameof (services));

            foreach (var service in services) {
                var factoryInterface = service.ImplementationInstance?.GetType ()
                    .GetTypeInfo ()
                    .GetInterfaces ()
                    .FirstOrDefault (i => i.GetTypeInfo ().IsGenericType &&
                        i.GetGenericTypeDefinition () == typeof (IServiceProviderFactory<>));

                if (factoryInterface == null) {
                    continue;
                }

                var containerBuilderType = factoryInterface.GenericTypeArguments[0];
                return (IServiceProvider) typeof (ServiceCollectionCommonExtensions)
                    .GetTypeInfo ()
                    .GetMethods ()
                    .Single (m => m.Name == nameof (BuildServiceProviderFromFactory) && m.IsGenericMethod)
                    .MakeGenericMethod (containerBuilderType)
                    .Invoke (null, new object[] { services, null });
            }

            return services.BuildServiceProvider ();
        }

        public static IServiceProvider BuildServiceProviderFromFactory<TContainerBuilder> ([NotNull] this IServiceCollection services, Action<TContainerBuilder> builderAction = null) {
            Check.NotNull (services, nameof (services));

            var serviceProviderFactory = services.GetSingletonInstanceOrNull<IServiceProviderFactory<TContainerBuilder>> ();
            if (serviceProviderFactory == null) {
                throw new RocketException ($"Could not find {typeof(IServiceProviderFactory<TContainerBuilder>).FullName} in {services}.");
            }

            var builder = serviceProviderFactory.CreateBuilder (services);
            builderAction?.Invoke (builder);
            return serviceProviderFactory.CreateServiceProvider (builder);
        }

        /// <summary>
        /// 从 <see cref="IServiceCollection"/> 获取依赖注入的对象实例，
        /// 此方法只能在依赖项注入注册阶段完成后使用。
        /// </summary>
        internal static T GetService<T> (this IServiceCollection services) {
            return services
                .GetSingletonInstance<IRocketApplication> ()
                .ServiceProvider
                .GetService<T> ();
        }

        /// <summary>
        /// 从 <see cref="IServiceCollection"/> 获取依赖注入的对象实例，
        /// 此方法只能在依赖项注入注册阶段完成后使用。
        /// </summary>
        internal static object GetService (this IServiceCollection services, Type type) {
            return services
                .GetSingletonInstance<IRocketApplication> ()
                .ServiceProvider
                .GetService (type);
        }

        /// <summary>
        /// 从 <see cref="IServiceCollection"/> 获取依赖注入的对象实例，
        /// 如果服务没有注册抛出异常，
        /// 此方法只能在依赖项注入注册阶段完成后使用。
        /// </summary>
        internal static T GetRequiredService<T> (this IServiceCollection services) {
            return services
                .GetSingletonInstance<IRocketApplication> ()
                .ServiceProvider
                .GetRequiredService<T> ();
        }

        /// <summary>
        /// 从 <see cref="IServiceCollection"/> 获取依赖注入的对象实例，
        /// 如果服务没有注册抛出异常，
        /// 此方法只能在依赖项注入注册阶段完成后使用。
        /// </summary>
        internal static object GetRequiredService (this IServiceCollection services, Type type) {
            return services
                .GetSingletonInstance<IRocketApplication> ()
                .ServiceProvider
                .GetRequiredService (type);
        }

        /// <summary>
        /// 从 <see cref="IServiceCollection"/> 获取依赖注入的延迟加载对象 <see cref="Lazy{T}"/> ，
        /// 只能在依赖项注入注册阶段完成后使用。
        /// </summary>
        public static Lazy<T> GetServiceLazy<T> (this IServiceCollection services) {
            return new Lazy<T> (services.GetService<T>, true);
        }

        /// <summary>
        /// 从 <see cref="IServiceCollection"/> 获取依赖注入的延迟加载对象 <see cref="Lazy{T}"/> ，
        /// 只能在依赖项注入注册阶段完成后使用。
        /// </summary>
        public static Lazy<object> GetServiceLazy (this IServiceCollection services, Type type) {
            return new Lazy<object> (() => services.GetService (type), true);
        }

        /// <summary>
        /// 从 <see cref="IServiceCollection"/> 获取依赖注入的延迟加载对象 <see cref="Lazy{T}"/> ，
        /// 只能在依赖项注入注册阶段完成后使用。
        /// </summary>
        public static Lazy<T> GetRequiredServiceLazy<T> (this IServiceCollection services) {
            return new Lazy<T> (services.GetRequiredService<T>, true);
        }

        /// <summary>
        /// 从 <see cref="IServiceCollection"/> 获取依赖注入的延迟加载对象 <see cref="Lazy{T}"/> ，
        /// 只能在依赖项注入注册阶段完成后使用。
        /// </summary>
        public static Lazy<object> GetRequiredServiceLazy (this IServiceCollection services, Type type) {
            return new Lazy<object> (() => services.GetRequiredService (type), true);
        }
    }
}