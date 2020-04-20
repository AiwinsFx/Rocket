using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using Aiwins.Rocket.BackgroundJobs;
using Aiwins.Rocket.Emailing.Localization;
using Aiwins.Rocket.Emailing.Templates;
using Aiwins.Rocket.Localization;
using Aiwins.Rocket.Modularity;
using Aiwins.Rocket.Settings;
using Aiwins.Rocket.VirtualFileSystem;

namespace Aiwins.Rocket.Emailing
{
    [DependsOn(
        typeof(RocketSettingsModule),
        typeof(RocketVirtualFileSystemModule),
        typeof(RocketBackgroundJobsAbstractionsModule),
        typeof(RocketLocalizationModule)
        )]
    public class RocketEmailingModule : RocketModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            AutoAddDefinitionProviders(context.Services);
        }

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<RocketVirtualFileSystemOptions>(options =>
            {
                options.FileSets.AddEmbedded<RocketEmailingModule>();
            });

            Configure<RocketLocalizationOptions>(options =>
            {
                options.Resources
                    .Add<EmailingResource>("en")
                    .AddVirtualJson("/Aiwins/Rocket/Emailing/Localization");
            });

            Configure<RocketBackgroundJobOptions>(options =>
            {
                options.AddJob<BackgroundEmailSendingJob>();
            });
        }

        private static void AutoAddDefinitionProviders(IServiceCollection services)
        {
            var definitionProviders = new List<Type>();

            services.OnRegistred(context =>
            {

                if (typeof(IEmailTemplateDefinitionProvider).IsAssignableFrom(context.ImplementationType))
                {
                    definitionProviders.Add(context.ImplementationType);
                }
            });

            services.Configure<RocketEmailTemplateOptions>(options =>
            {
                options.DefinitionProviders.AddIfNotContains(definitionProviders);
            });
        }

        public override void OnApplicationInitialization(ApplicationInitializationContext context)
        {
            using (var scope = context.ServiceProvider.CreateScope())
            {
                var emailTemplateDefinitionManager =
                    scope.ServiceProvider.GetRequiredService<IEmailTemplateDefinitionManager>();

                foreach (var templateDefinition in emailTemplateDefinitionManager.GetAll())
                {
                    foreach (var contributor in templateDefinition.Contributors)
                    {
                        contributor.Initialize(new EmailTemplateInitializationContext(templateDefinition, scope.ServiceProvider));
                    }
                }
            }
        }
    }
}
