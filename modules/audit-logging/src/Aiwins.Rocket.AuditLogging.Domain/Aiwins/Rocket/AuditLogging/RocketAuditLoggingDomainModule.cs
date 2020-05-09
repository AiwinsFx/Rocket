using Aiwins.Rocket.Auditing;
using Aiwins.Rocket.Domain;
using Aiwins.Rocket.Modularity;
using Aiwins.Rocket.ObjectExtending;
using Aiwins.Rocket.ObjectExtending.Modularity;

namespace Aiwins.Rocket.AuditLogging {
    [DependsOn (typeof (RocketAuditingModule))]
    [DependsOn (typeof (RocketDddDomainModule))]
    [DependsOn (typeof (RocketAuditLoggingDomainSharedModule))]
    public class RocketAuditLoggingDomainModule : RocketModule {
        public override void PostConfigureServices (ServiceConfigurationContext context) {
            ModuleExtensionConfigurationHelper.ApplyEntityConfigurationToEntity (
                AuditLoggingModuleExtensionConsts.ModuleName,
                AuditLoggingModuleExtensionConsts.EntityNames.AuditLog,
                typeof (AuditLog)
            );

            ModuleExtensionConfigurationHelper.ApplyEntityConfigurationToEntity (
                AuditLoggingModuleExtensionConsts.ModuleName,
                AuditLoggingModuleExtensionConsts.EntityNames.AuditLogAction,
                typeof (AuditLogAction)
            );

            ModuleExtensionConfigurationHelper.ApplyEntityConfigurationToEntity (
                AuditLoggingModuleExtensionConsts.ModuleName,
                AuditLoggingModuleExtensionConsts.EntityNames.EntityChange,
                typeof (EntityChange)
            );
        }
    }
}