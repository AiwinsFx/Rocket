using System.Collections.Generic;
using JetBrains.Annotations;

namespace Aiwins.Rocket.Settings {
    public interface ISettingDefinitionManager {
        [NotNull]
        SettingDefinition Get ([NotNull] string name);

        IReadOnlyList<SettingDefinition> GetAll ();

        SettingDefinition GetOrNull (string name);
    }
}