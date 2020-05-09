using System.Collections.Generic;

namespace Aiwins.Rocket.Data {
    //TODO: Move to Aiwins.Rocket.Data.ObjectExtending namespace at v3.0

    public interface IHasExtraProperties {
        Dictionary<string, object> ExtraProperties { get; }
    }
}