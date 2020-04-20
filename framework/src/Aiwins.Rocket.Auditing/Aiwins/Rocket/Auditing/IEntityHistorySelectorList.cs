using System.Collections.Generic;

namespace Aiwins.Rocket.Auditing {
    public interface IEntityHistorySelectorList : IList<NamedTypeSelector> {
        bool RemoveByName (string name);
    }
}