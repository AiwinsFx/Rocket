using System;
using System.Collections.Generic;
using System.Globalization;
using Localization.Resources.RocketUi;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MyCompanyName.MyProjectName.Localization;
using MyCompanyName.MyProjectName.Web;
using MyCompanyName.MyProjectName.Web.Menus;
using Aiwins.Rocket;
using Aiwins.Rocket.AspNetCore.TestBase;
using Aiwins.Rocket.Localization;
using Aiwins.Rocket.Modularity;
using Aiwins.Rocket.UI.Navigation;
using Aiwins.Rocket.Validation.Localization;

namespace MyCompanyName.MyProjectName
{
    [DependsOn(
        typeof(RocketAspNetCoreTestBaseModule),
        typeof(MyProjectNameWebModule),
        typeof(MyProjectNameApplicationTestModule)
    )]
    public class MyProjectNameWebTestModule : RocketModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.PreConfigure<IMvcBuilder>(builder =>
            {
                builder.PartManager.ApplicationParts.Add(new AssemblyPart(typeof(MyProjectNameWebModule).Assembly));
            });
        }

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            ConfigureLocalizationServices(context.Services);
            ConfigureNavigationServices(context.Services);
        }

        private static void ConfigureLocalizationServices(IServiceCollection services)
        {
            var cultures = new List<CultureInfo> { new CultureInfo("zh-Hans"), new CultureInfo("en") };
            services.Configure<RequestLocalizationOptions>(options =>
            {
                options.DefaultRequestCulture = new RequestCulture("zh-Hans");
                options.SupportedCultures = cultures;
                options.SupportedUICultures = cultures;
            });

            services.Configure<RocketLocalizationOptions>(options =>
            {
                options.Resources
                    .Get<MyProjectNameResource>()
                    .AddBaseTypes(
                        typeof(RocketValidationResource),
                        typeof(RocketUiResource)
                    );
            });
        }

        private static void ConfigureNavigationServices(IServiceCollection services)
        {
            services.Configure<RocketNavigationOptions>(options =>
            {
                options.MenuContributors.Add(new MyProjectNameMenuContributor());
            });
        }

        public override void OnApplicationInitialization(ApplicationInitializationContext context)
        {
            var app = context.GetApplicationBuilder();
            var env = context.GetEnvironment();

            app.Use(async (ctx, next) =>
            {
                try
                {
                    await next();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            });

            app.UseVirtualFiles();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseRocketRequestLocalization();

            app.Use(async (ctx, next) =>
            {
                try
                {
                    await next();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            });

            app.UseConfiguredEndpoints();
        }
    }
}