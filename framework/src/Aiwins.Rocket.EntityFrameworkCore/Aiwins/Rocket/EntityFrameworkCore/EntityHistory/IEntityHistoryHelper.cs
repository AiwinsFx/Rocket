using System.Collections.Generic;
using Aiwins.Rocket.Auditing;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Aiwins.Rocket.EntityFrameworkCore.EntityHistory {
    public interface IEntityHistoryHelper {
        List<EntityChangeInfo> CreateChangeList (ICollection<EntityEntry> entityEntries);

        void UpdateChangeList (List<EntityChangeInfo> entityChanges);
    }
}