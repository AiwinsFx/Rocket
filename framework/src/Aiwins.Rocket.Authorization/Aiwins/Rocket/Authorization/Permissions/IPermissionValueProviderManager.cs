using System.Collections.Generic;

namespace Aiwins.Rocket.Authorization.Permissions {
    public interface IPermissionValueProviderManager {
        IReadOnlyList<IPermissionValueProvider> ValueProviders { get; }
    }
}