using System.Threading.Tasks;

namespace Aiwins.Rocket.Auditing {
    public interface IAuditingStore {
        Task SaveAsync (AuditLogInfo auditInfo);
    }
}