using System;
using JetBrains.Annotations;

namespace Aiwins.Rocket.MultiTenancy {
    public interface ICurrentTenant {
        bool IsAvailable { get; }

        [CanBeNull]
        Guid? Id { get; }

        [CanBeNull]
        string Name { get; }

        IDisposable Change (Guid? id, string name = null);
    }
}