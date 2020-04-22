using System;
using System.Collections.Generic;
using System.Reflection;
using Aiwins.Rocket.Http.Modeling;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Http;

namespace Aiwins.Rocket.AspNetCore.Mvc.Conventions {
    public class RocketConventionalControllerOptions {
        public ConventionalControllerSettingList ConventionalControllerSettings { get; }

        public List<Type> FormBodyBindingIgnoredTypes { get; }

        public RocketConventionalControllerOptions () {
            ConventionalControllerSettings = new ConventionalControllerSettingList ();

            FormBodyBindingIgnoredTypes = new List<Type> {
                typeof (IFormFile)
            };
        }

        public RocketConventionalControllerOptions Create (
            Assembly assembly, [CanBeNull] Action<ConventionalControllerSetting> optionsAction = null) {
            var setting = new ConventionalControllerSetting (
                assembly,
                ModuleApiDescriptionModel.DefaultRootPath,
                ModuleApiDescriptionModel.DefaultRemoteServiceName
            );

            optionsAction?.Invoke (setting);
            setting.Initialize ();
            ConventionalControllerSettings.Add (setting);
            return this;
        }
    }
}