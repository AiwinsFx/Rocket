﻿using System;
using System.Collections.Generic;

namespace Aiwins.Rocket.ObjectExtending.Modularity {
    public static class ModuleExtensionConfigurationExtensions {
        public static T ConfigureEntity<T> (
            this T objectConfiguration,
            string objectName,
            Action<EntityExtensionConfiguration> configureAction)
        where T : ModuleExtensionConfiguration {
            var configuration = objectConfiguration.Entities.GetOrAdd (
                objectName,
                () => new EntityExtensionConfiguration ()
            );

            configureAction (configuration);

            return objectConfiguration;
        }
    }
}