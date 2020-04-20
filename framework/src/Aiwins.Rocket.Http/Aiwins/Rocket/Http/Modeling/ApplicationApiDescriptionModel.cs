using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace Aiwins.Rocket.Http.Modeling {
    [Serializable]
    public class ApplicationApiDescriptionModel {
        public IDictionary<string, ModuleApiDescriptionModel> Modules { get; set; }

        public IDictionary<string, TypeApiDescriptionModel> Types { get; set; }

        private ApplicationApiDescriptionModel () {

        }

        public static ApplicationApiDescriptionModel Create () {
            return new ApplicationApiDescriptionModel {
                Modules = new Dictionary<string, ModuleApiDescriptionModel> (), //TODO: Why ConcurrentDictionary?
                    Types = new Dictionary<string, TypeApiDescriptionModel> ()
            };
        }

        public ModuleApiDescriptionModel AddModule (ModuleApiDescriptionModel module) {
            if (Modules.ContainsKey (module.RootPath)) {
                throw new RocketException ("There is already a module with same root path: " + module.RootPath);
            }

            return Modules[module.RootPath] = module;
        }

        public ModuleApiDescriptionModel GetOrAddModule (string rootPath, string remoteServiceName) {
            return Modules.GetOrAdd (rootPath, () => ModuleApiDescriptionModel.Create (rootPath, remoteServiceName));
        }

        public ApplicationApiDescriptionModel CreateSubModel (string[] modules = null, string[] controllers = null, string[] actions = null) {
            var subModel = new ApplicationApiDescriptionModel ();

            foreach (var module in Modules.Values) {
                if (modules == null || modules.Contains (module.RootPath)) {
                    subModel.AddModule (module.CreateSubModel (controllers, actions));
                }
            }

            return subModel;
        }
    }
}