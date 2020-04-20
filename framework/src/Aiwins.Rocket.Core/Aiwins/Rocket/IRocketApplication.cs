using System;
using Aiwins.Rocket.Modularity;
using Microsoft.Extensions.DependencyInjection;

namespace Aiwins.Rocket {
    public interface IRocketApplication : IModuleContainer, IDisposable {
        /// <summary>
        /// 应用程序启动模块的类型。
        /// </summary>
        Type StartupModuleType { get; }

        /// <summary>
        /// 已注册到应用程序的服务列表，
        /// 应用程序初始化后将不能注入新的服务。
        /// </summary>
        IServiceCollection Services { get; }

        /// <summary>
        /// 应用程序的服务提供商，
        /// 应用程序初始化前不可使用。
        /// </summary>
        IServiceProvider ServiceProvider { get; }

        /// <summary>
        /// 用于正常关闭应用程序和所有模块的方法。
        /// </summary>
        void Shutdown ();
    }
}