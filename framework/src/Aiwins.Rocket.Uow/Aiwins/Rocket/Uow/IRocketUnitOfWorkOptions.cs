using System;
using System.Data;

namespace Aiwins.Rocket.Uow {
    public interface IRocketUnitOfWorkOptions {
        bool IsTransactional { get; }

        IsolationLevel? IsolationLevel { get; }

        TimeSpan? Timeout { get; }
    }
}