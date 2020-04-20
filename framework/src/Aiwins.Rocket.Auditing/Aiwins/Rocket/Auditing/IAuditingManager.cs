using JetBrains.Annotations;

namespace Aiwins.Rocket.Auditing {
    public interface IAuditingManager {
        [CanBeNull]
        IAuditLogScope Current { get; }

        IAuditLogSaveHandle BeginScope ();
    }
}