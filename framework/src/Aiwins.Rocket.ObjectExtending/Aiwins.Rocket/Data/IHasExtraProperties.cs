using System.Collections.Generic;

namespace Aiwins.Rocket.Data {
    public interface IHasExtraProperties {
        Dictionary<string, object> ExtraProperties { get; }
    }
}