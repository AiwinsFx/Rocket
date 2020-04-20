using System.Threading.Tasks;
using Aiwins.Rocket.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

namespace Aiwins.Rocket.Auditing {
    [Dependency (TryRegister = true)]
    public class SimpleLogAuditingStore : IAuditingStore, ISingletonDependency {
        public ILogger<SimpleLogAuditingStore> Logger { get; set; }

        public SimpleLogAuditingStore () {
            Logger = NullLogger<SimpleLogAuditingStore>.Instance;
        }

        public Task SaveAsync (AuditLogInfo auditInfo) {
            Logger.LogInformation (auditInfo.ToString ());
            return Task.FromResult (0);
        }
    }
}