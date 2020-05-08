using System;
using System.Threading.Tasks;
using Aiwins.Rocket.Auditing;
using Aiwins.Rocket.DependencyInjection;
using Aiwins.Rocket.Guids;
using Aiwins.Rocket.Uow;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;

namespace Aiwins.Rocket.AuditLogging {
    public class AuditingStore : IAuditingStore, ITransientDependency {
        public ILogger<AuditingStore> Logger { get; set; }

        protected IAuditLogRepository AuditLogRepository { get; }
        protected IGuidGenerator GuidGenerator { get; }
        protected IUnitOfWorkManager UnitOfWorkManager { get; }
        protected RocketAuditingOptions Options { get; }

        public AuditingStore (
            IAuditLogRepository auditLogRepository,
            IGuidGenerator guidGenerator,
            IUnitOfWorkManager unitOfWorkManager,
            IOptions<RocketAuditingOptions> options) {
            AuditLogRepository = auditLogRepository;
            GuidGenerator = guidGenerator;
            UnitOfWorkManager = unitOfWorkManager;
            Options = options.Value;

            Logger = NullLogger<AuditingStore>.Instance;
        }

        public virtual async Task SaveAsync (AuditLogInfo auditInfo) {
            if (!Options.HideErrors) {
                await SaveLogAsync (auditInfo);
                return;
            }

            try {
                await SaveLogAsync (auditInfo);
            } catch (Exception ex) {
                Logger.LogWarning ("Could not save the audit log object: " + Environment.NewLine + auditInfo.ToString ());
                Logger.LogException (ex, LogLevel.Error);
            }
        }

        protected virtual async Task SaveLogAsync (AuditLogInfo auditInfo) {
            using (var uow = UnitOfWorkManager.Begin (true)) {
                await AuditLogRepository.InsertAsync (new AuditLog (GuidGenerator, auditInfo));
                await uow.SaveChangesAsync ();
            }
        }
    }
}