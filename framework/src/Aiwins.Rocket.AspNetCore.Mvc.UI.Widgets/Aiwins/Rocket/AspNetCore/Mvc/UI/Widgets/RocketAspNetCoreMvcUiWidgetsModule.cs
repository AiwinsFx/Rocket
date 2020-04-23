using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Microsoft.Extensions.DependencyInjection;
using Aiwins.Rocket.AspNetCore.Mvc.UI.Bootstrap;
using Aiwins.Rocket.AspNetCore.Mvc.UI.Bundling;
using Aiwins.Rocket.Modularity;
using Aiwins.Rocket.VirtualFileSystem;

namespace Aiwins.Rocket.AspNetCore.Mvc.UI.Widgets
{
    [DependsOn(
        typeof(RocketAspNetCoreMvcUiBootstrapModule),
        typeof(RocketAspNetCoreMvcUiBundlingModule)
    )]
    public class RocketAspNetCoreMvcUiWidgetsModule : RocketModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            PreConfigure<IMvcBuilder>(mvcBuilder =>
            {
                mvcBuilder.AddApplicationPartIfNotExists(typeof(RocketAspNetCoreMvcUiWidgetsModule).Assembly);
            });

            AutoAddWidgets(context.Services);
        }

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddTransient<DefaultViewComponentHelper>();

            Configure<RocketVirtualFileSystemOptions>(options =>
            {
                options.FileSets.AddEmbedded<RocketAspNetCoreMvcUiWidgetsModule>();
            });
        }

        private static void AutoAddWidgets(IServiceCollection services)
        {
            var widgetTypes = new List<Type>();

            services.OnRegistred(context =>
            {
                if (WidgetAttribute.IsWidget(context.ImplementationType))
                {
                    widgetTypes.Add(context.ImplementationType);
                }
            });

            services.Configure<RocketWidgetOptions>(options =>
            {
                foreach (var widgetType in widgetTypes)
                {
                    options.Widgets.Add(new WidgetDefinition(widgetType));
                }
            });
        }
    }
}
