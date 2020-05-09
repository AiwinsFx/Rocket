﻿using System;
using System.Collections.Generic;
using Aiwins.Rocket.Localization;
using JetBrains.Annotations;

namespace Aiwins.Rocket.ObjectExtending.Modularity {
    public class ExtensionPropertyConfiguration : IHasNameWithLocalizableDisplayName {
        [NotNull]
        public EntityExtensionConfiguration EntityExtensionConfiguration { get; }

        [NotNull]
        public string Name { get; }

        [NotNull]
        public Type Type { get; }

        [NotNull]
        public List<Attribute> Attributes { get; }

        [NotNull]
        public List<Action<ObjectExtensionPropertyValidationContext>> Validators { get; }

        [CanBeNull]
        public ILocalizableString DisplayName { get; set; }

        [NotNull]
        public Dictionary<string, object> Configuration { get; }

        /// <summary>
        /// Single point to enable/disable this property for the clients (UI and API).
        /// If this is false, the configuration made in the <see cref="UI"/> and the <see cref="Api"/>
        /// properties are not used.
        /// Default: true.
        /// </summary>
        public bool IsAvailableToClients { get; set; } = true;

        [NotNull]
        public ExtensionPropertyEntityConfiguration Entity { get; }

        [NotNull]
        public ExtensionPropertyUiConfiguration UI { get; }

        [NotNull]
        public ExtensionPropertyApiConfiguration Api { get; }

        public ExtensionPropertyConfiguration (
            [NotNull] EntityExtensionConfiguration entityExtensionConfiguration, [NotNull] Type type, [NotNull] string name) {
            EntityExtensionConfiguration = Check.NotNull (entityExtensionConfiguration, nameof (entityExtensionConfiguration));
            Type = Check.NotNull (type, nameof (type));
            Name = Check.NotNull (name, nameof (name));

            Configuration = new Dictionary<string, object> ();
            Attributes = new List<Attribute> ();
            Validators = new List<Action<ObjectExtensionPropertyValidationContext>> ();

            Entity = new ExtensionPropertyEntityConfiguration ();
            UI = new ExtensionPropertyUiConfiguration ();
            Api = new ExtensionPropertyApiConfiguration ();
        }
    }
}