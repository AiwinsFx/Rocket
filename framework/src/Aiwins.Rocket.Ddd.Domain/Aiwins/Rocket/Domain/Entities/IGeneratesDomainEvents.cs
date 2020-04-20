using System.Collections.Generic;

namespace Aiwins.Rocket.Domain.Entities {
    //TODO: 需要优化

    public interface IGeneratesDomainEvents {
        IEnumerable<object> GetLocalEvents ();

        IEnumerable<object> GetDistributedEvents ();

        void ClearLocalEvents ();

        void ClearDistributedEvents ();
    }
}