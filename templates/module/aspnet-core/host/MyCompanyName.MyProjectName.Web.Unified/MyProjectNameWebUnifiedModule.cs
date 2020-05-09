using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MyCompanyName.MyProjectName.EntityFrameworkCore;
using MyCompanyName.MyProjectName.MultiTenancy;
using MyCompanyName.MyProjectName.Web;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Swagger;
using Aiwins.Rocket;
using Aiwins.Rocket.Account;
using Aiwins.Rocket.Account.Web;
using Aiwins.Rocket.AspNetCore.Mvc.UI.Theme.Basic;
using Aiwins.Rocket.AspNetCore.Mvc.UI.Theme.Shared;
using Aiwins.Rocket.AspNetCore.Serilog;
using Aiwins.Rocket.AuditLogging.EntityFrameworkCore;
using Aiwins.Rocket.Autofac;
using Aiwins.Rocket.Data;
using Aiwins.Rocket.EntityFrameworkCore;
using Aiwins.Rocket.EntityFrameworkCore.SqlServer;
using Aiwins.Rocket.FeatureManagement;
using Aiwins.Rocket.Identity;
using Aiwins.Rocket.Identity.EntityFrameworkCore;
using Aiwins.Rocket.Identity.Web;
using Aiwins.Rocket.Localization;
using Aiwins.Rocket.Modularity;
using Aiwins.Rocket.MultiTenancy;
using Aiwins.Rocket.PermissionManagement;
using Aiwins.Rocket.PermissionManagement.EntityFrameworkCore;
using Aiwins.Rocket.PermissionManagement.Identity;
using Aiwins.Rocket.SettingManagement.EntityFrameworkCore;
using Aiwins.Rocket.TenantManagement;
using Aiwins.Rocket.TenantManagement.EntityFrameworkCore;
using Aiwins.Rocket.TenantManagement.Web;
using Aiwins.Rocket.Threading;
using Aiwins.Rocket.VirtualFileSystem;

namespace MyCompanyName.MyProjectName
{
    [DependsOn(
        typeof(MyProjectNameWebModule),
        typeof(MyProjectNameApplicationModule),
        typeof(MyProjectNameEntityFrameworkCoreModule),
        typeof(RocketAuditLoggingEntityFrameworkCoreModule),
        typeof(RocketAutofacModule),
        typeof(RocketAccountWebModule),
        typeof(RocketAccountApplicationModule),
        typeof(RocketEntityFrameworkCoreSqlServerModule),
        typeof(RocketSettingManagementEntityFrameworkCoreModule),
        typeof(RocketPermissionManagementEntityFrameworkCoreModule),
        typeof(RocketPermissionManagementApplicationModule),
        typeof(RocketIdentityWebModule),
        typeof(RocketIdentityApplicationModule),
        typeof(RocketIdentityEntityFrameworkCoreModule),
        typeof(RocketPermissionManagementDomainIdentityModule),
        typeof(RocketFeatureManagementApplicationModule),
        typeof(RocketTenantManagementWebModule),
        typeof(RocketTenantManagementApplicationModule),
        typeof(RocketTenantManagementEntityFrameworkCoreModule),
        typeof(RocketAspNetCoreMvcUiBasicThemeModule),
        typeof(RocketAspNetCoreSerilogModule)
        )]
    public class MyProjectNameWebUnifiedModule : RocketModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            var hostingEnvironment = context.Services.GetHostingEnvironment();
            var configuration = context.Services.GetConfiguration();

            Configure<RocketDbContextOptions>(options =>
            {
                options.UseSqlServer();
            });

            if (hostingEnvironment.IsDevelopment())
            {
                Configure<RocketVirtualFileSystemOptions>(options =>
                {
                    options.FileSets.ReplaceEmbeddedByPhysical<MyProjectNameDomainSharedModule>(Path.Combine(hostingEnvironment.ContentRootPath, string.Format("..{0}..{0}src{0}MyCompanyName.MyProjectName.Domain.Shared", Path.DirectorySeparatorChar)));
                    options.FileSets.ReplaceEmbeddedByPhysical<MyProjectNameDomainModule>(Path.Combine(hostingEnvironment.ContentRootPath, string.Format("..{0}..{0}src{0}MyCompanyName.MyProjectName.Domain", Path.DirectorySeparatorChar)));
                    options.FileSets.ReplaceEmbeddedByPhysical<MyProjectNameApplicationContractsModule>(Path.Combine(hostingEnvironment.ContentRootPath, string.Format("..{0}..{0}src{0}MyCompanyName.MyProjectName.Application.Contracts", Path.DirectorySeparatorChar)));
                    options.FileSets.ReplaceEmbeddedByPhysical<MyProjectNameApplicationModule>(Path.Combine(hostingEnvironment.ContentRootPath, string.Format("..{0}..{0}src{0}MyCompanyName.MyProjectName.Application", Path.DirectorySeparatorChar)));
                    options.FileSets.ReplaceEmbeddedByPhysical<MyProjectNameWebModule>(Path.Combine(hostingEnvironment.ContentRootPath, string.Format("..{0}..{0}src{0}MyCompanyName.MyProjectName.Web", Path.DirectorySeparatorChar)));
                });
            }

            context.Services.AddSwaggerGen(
                options =>
                {
                    options.SwaggerDoc("v1", new OpenApiInfo { Title = "MyProjectName API", Version = "v1" });
                    options.DocInclusionPredicate((docName, description) => true);
                    options.CustomSchemaIds(type => type.FullName);
                });

            Configure<RocketLocalizationOptions>(options =>
            {
                options.Languages.Add(new LanguageInfo("cs", "cs", "Čeština"));
                options.Languages.Add(new LanguageInfo("en", "en", "English"));
                options.Languages.Add(new LanguageInfo("pt-BR", "pt-BR", "Português (Brasil)"));
                options.Languages.Add(new LanguageInfo("ru", "ru", "Русский"));
                options.Languages.Add(new LanguageInfo("tr", "tr", "Türkçe"));
                options.Languages.Add(new LanguageInfo("zh-Hans", "zh-Hans", "简体中文"));
                options.Languages.Add(new LanguageInfo("zh-Hant", "zh-Hant", "繁體中文"));
            });

            Configure<RocketMultiTenancyOptions>(options =>
            {
                options.IsEnabled = MultiTenancyConsts.IsEnabled;
            });
        }

        public override void OnApplicationInitialization(ApplicationInitializationContext context)
        {
            var app = context.GetApplicationBuilder();

            if (context.GetEnvironment().IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseErrorPage();
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseVirtualFiles();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            if (MultiTenancyConsts.IsEnabled)
            {
                app.UseMultiTenancy();
            }

            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "Support APP API");
            });

            app.UseRocketRequestLocalization();
            app.UseAuditing();
            app.UseRocketSerilogEnrichers();
            app.UseMvcWithDefaultRouteAndArea();

            using (var scope = context.ServiceProvider.CreateScope())
            {
                AsyncHelper.RunSync(async () =>
                {
                    await scope.ServiceProvider
                        .GetRequiredService<IDataSeeder>()
                        .SeedAsync();
                });
            }
        }
    }
}
