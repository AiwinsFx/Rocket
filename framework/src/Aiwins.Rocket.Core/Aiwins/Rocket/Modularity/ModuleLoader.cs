using System;
using System.Collections.Generic;
using System.Linq;
using Aiwins.Rocket.Modularity.PlugIns;
using Microsoft.Extensions.DependencyInjection;

namespace Aiwins.Rocket.Modularity {
    public class ModuleLoader : IModuleLoader {
        public IRocketModuleDescriptor[] LoadModules (
            IServiceCollection services,
            Type startupModuleType,
            PlugInSourceList plugInSources) {
            Check.NotNull (services, nameof (services));
            Check.NotNull (startupModuleType, nameof (startupModuleType));
            Check.NotNull (plugInSources, nameof (plugInSources));

            var modules = GetDescriptors (services, startupModuleType, plugInSources);

            modules = SortByDependency (modules, startupModuleType);
            ConfigureServices (modules, services);

            return modules.ToArray ();
        }

        private List<IRocketModuleDescriptor> GetDescriptors (
            IServiceCollection services,
            Type startupModuleType,
            PlugInSourceList plugInSources) {
            var modules = new List<RocketModuleDescriptor> ();

            FillModules (modules, services, startupModuleType, plugInSources);
            SetDependencies (modules);

            return modules.Cast<IRocketModuleDescriptor> ().ToList ();
        }

        protected virtual void FillModules (
            List<RocketModuleDescriptor> modules,
            IServiceCollection services,
            Type startupModuleType,
            PlugInSourceList plugInSources) {
            // 从启动模块开始查找所有模块
            foreach (var moduleType in RocketModuleHelper.FindAllModuleTypes (startupModuleType)) {
                modules.Add (CreateModuleDescriptor (services, moduleType));
            }

            // 插件模块
            foreach (var moduleType in plugInSources.GetAllModules ()) {
                if (modules.Any (m => m.Type == moduleType)) {
                    continue;
                }

                modules.Add (CreateModuleDescriptor (services, moduleType, isLoadedAsPlugIn : true));
            }
        }

        protected virtual void SetDependencies (List<RocketModuleDescriptor> modules) {
            foreach (var module in modules) {
                SetDependencies (modules, module);
            }
        }

        protected virtual List<IRocketModuleDescriptor> SortByDependency (List<IRocketModuleDescriptor> modules, Type startupModuleType) {
            var sortedModules = modules.SortByDependencies (m => m.Dependencies);
            sortedModules.MoveItem (m => m.Type == startupModuleType, modules.Count - 1);
            return sortedModules;
        }

        protected virtual RocketModuleDescriptor CreateModuleDescriptor (IServiceCollection services, Type moduleType, bool isLoadedAsPlugIn = false) {
            return new RocketModuleDescriptor (moduleType, CreateAndRegisterModule (services, moduleType), isLoadedAsPlugIn);
        }

        protected virtual IRocketModule CreateAndRegisterModule (IServiceCollection services, Type moduleType) {
            var module = (IRocketModule) Activator.CreateInstance (moduleType);
            services.AddSingleton (moduleType, module);
            return module;
        }

        protected virtual void ConfigureServices (List<IRocketModuleDescriptor> modules, IServiceCollection services) {
            var context = new ServiceConfigurationContext (services);
            services.AddSingleton (context);

            foreach (var module in modules) {
                if (module.Instance is RocketModule rocketModule) {
                    rocketModule.ServiceConfigurationContext = context;
                }
            }

            // 预配置服务
            foreach (var module in modules.Where (m => m.Instance is IPreConfigureServices)) {
                ((IPreConfigureServices) module.Instance).PreConfigureServices (context);
            }

            // 配置服务
            foreach (var module in modules) {
                if (module.Instance is RocketModule rocketModule) {
                    if (!rocketModule.SkipAutoServiceRegistration) {
                        services.AddAssembly (module.Type.Assembly);
                    }
                }

                module.Instance.ConfigureServices (context);
            }

            // 延时配置服务
            foreach (var module in modules.Where (m => m.Instance is IPostConfigureServices)) {
                ((IPostConfigureServices) module.Instance).PostConfigureServices (context);
            }

            foreach (var module in modules) {
                if (module.Instance is RocketModule rocketModule) {
                    rocketModule.ServiceConfigurationContext = null;
                }
            }
        }

        protected virtual void SetDependencies (List<RocketModuleDescriptor> modules, RocketModuleDescriptor module) {
            foreach (var dependedModuleType in RocketModuleHelper.FindDependedModuleTypes (module.Type)) {
                var dependedModule = modules.FirstOrDefault (m => m.Type == dependedModuleType);
                if (dependedModule == null) {
                    throw new RocketException ("Could not find a depended module " + dependedModuleType.AssemblyQualifiedName + " for " + module.Type.AssemblyQualifiedName);
                }

                module.AddDependency (dependedModule);
            }
        }
    }
}