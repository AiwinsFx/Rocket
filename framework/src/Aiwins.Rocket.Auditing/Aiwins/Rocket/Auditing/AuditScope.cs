using JetBrains.Annotations;

namespace Aiwins.Rocket.Auditing {
    public interface IAuditLogScope {
        [NotNull]
        AuditLogInfo Log { get; }
    }
}