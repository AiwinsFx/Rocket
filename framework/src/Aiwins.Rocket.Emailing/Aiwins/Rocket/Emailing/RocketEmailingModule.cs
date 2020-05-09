using Aiwins.Rocket.BackgroundJobs;
using Aiwins.Rocket.Emailing.Localization;
using Aiwins.Rocket.Localization;
using Aiwins.Rocket.Modularity;
using Aiwins.Rocket.Settings;
using Aiwins.Rocket.TextTemplating;
using Aiwins.Rocket.VirtualFileSystem;

namespace Aiwins.Rocket.Emailing {
    [DependsOn (
        typeof (RocketSettingsModule),
        typeof (RocketVirtualFileSystemModule),
        typeof (RocketBackgroundJobsAbstractionsModule),
        typeof (RocketLocalizationModule),
        typeof (RocketTextTemplatingModule)
    )]
    public class RocketEmailingModule : RocketModule {
        public override void ConfigureServices (ServiceConfigurationContext context) {
            Configure<RocketVirtualFileSystemOptions> (options => {
                options.FileSets.AddEmbedded<RocketEmailingModule> ();
            });

            Configure<RocketLocalizationOptions> (options => {
                options.Resources
                    .Add<EmailingResource> ("en")
                    .AddVirtualJson ("/Aiwins/Rocket/Emailing/Localization");
            });

            Configure<RocketBackgroundJobOptions> (options => {
                options.AddJob<BackgroundEmailSendingJob> ();
            });
        }
    }
}