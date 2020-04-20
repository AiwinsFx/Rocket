using System.Collections.Generic;

namespace Aiwins.Rocket.Settings {
    public interface ISettingValueProviderManager {
        List<ISettingValueProvider> Providers { get; }
    }
}