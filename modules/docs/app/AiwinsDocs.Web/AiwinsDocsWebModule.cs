using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Swagger;
using Aiwins.Rocket;
using Aiwins.Rocket.Account.Web;
using Aiwins.Rocket.AspNetCore.Mvc.Localization;
using Aiwins.Rocket.AspNetCore.Mvc.UI;
using Aiwins.Rocket.AspNetCore.Mvc.UI.Bootstrap;
using Aiwins.Rocket.AspNetCore.Mvc.UI.Theme.Basic;
using Aiwins.Rocket.AspNetCore.Mvc.UI.Theme.Shared;
using Aiwins.Rocket.AspNetCore.Mvc.UI.Theming;
using Aiwins.Rocket.Autofac;
using Aiwins.Rocket.Data;
using Aiwins.Rocket.EntityFrameworkCore;
using Aiwins.Rocket.Identity;
using Aiwins.Rocket.Identity.Web;
using Aiwins.Rocket.Localization;
using Aiwins.Rocket.Modularity;
using Aiwins.Rocket.PermissionManagement;
using Aiwins.Rocket.PermissionManagement.Identity;
using Aiwins.Rocket.Threading;
using Aiwins.Rocket.UI;
using Aiwins.Rocket.VirtualFileSystem;
using Aiwins.Docs;
using Aiwins.Docs.Admin;
using Aiwins.Docs.Localization;
using AiwinsDocs.EntityFrameworkCore;
using Localization.Resources.RocketUi;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Aiwins.Rocket.Account;
using Aiwins.Rocket.Validation.Localization;
using Aiwins.Docs.Documents.FullSearch.Elastic;

namespace AiwinsDocs.Web
{
    [DependsOn(
        typeof(DocsWebModule),
        typeof(DocsAdminWebModule),
        typeof(DocsApplicationModule),
        typeof(DocsAdminApplicationModule),
        typeof(AiwinsDocsEntityFrameworkCoreModule),
        typeof(RocketAutofacModule),
        typeof(RocketAccountWebModule),
        typeof(RocketAccountApplicationModule),
        typeof(RocketIdentityWebModule),
        typeof(RocketIdentityApplicationModule),
        typeof(RocketPermissionManagementDomainIdentityModule),
        typeof(RocketPermissionManagementApplicationModule),
        typeof(RocketAspNetCoreMvcUiBasicThemeModule)
    )]
    public class AiwinsDocsWebModule : RocketModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            PreConfigure<RocketMvcDataAnnotationsLocalizationOptions>(options =>
            {
                options.AddAssemblyResource(typeof(DocsResource), typeof(AiwinsDocsWebModule).Assembly);
            });
        }

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            var hostingEnvironment = context.Services.GetHostingEnvironment();
            var configuration = context.Services.GetConfiguration();

            Configure<DocsUiOptions>(options =>
            {
                options.RoutePrefix = null;
            });

            Configure<DocsElasticSearchOptions>(options =>
            {
                options.Enable = true;
            });

            Configure<RocketDbConnectionOptions>(options =>
            {
                options.ConnectionStrings.Default = configuration["ConnectionString"];
            });

            Configure<RocketDbContextOptions>(options =>
            {
                options.UseSqlServer();
            });

            if (hostingEnvironment.IsDevelopment())
            {
                Configure<RocketVirtualFileSystemOptions>(options =>
                {
                    options.FileSets.ReplaceEmbeddedByPhysical<RocketUiModule>(Path.Combine(hostingEnvironment.ContentRootPath, string.Format("..{0}..{0}..{0}..{0}framework{0}src{0}Aiwins.Rocket.UI", Path.DirectorySeparatorChar)));
                    options.FileSets.ReplaceEmbeddedByPhysical<RocketAspNetCoreMvcUiModule>(Path.Combine(hostingEnvironment.ContentRootPath, string.Format("..{0}..{0}..{0}..{0}framework{0}src{0}Aiwins.Rocket.AspNetCore.Mvc.UI", Path.DirectorySeparatorChar)));
                    options.FileSets.ReplaceEmbeddedByPhysical<RocketAspNetCoreMvcUiBootstrapModule>(Path.Combine(hostingEnvironment.ContentRootPath, string.Format("..{0}..{0}..{0}..{0}framework{0}src{0}Aiwins.Rocket.AspNetCore.Mvc.UI.Bootstrap", Path.DirectorySeparatorChar)));
                    options.FileSets.ReplaceEmbeddedByPhysical<RocketAspNetCoreMvcUiThemeSharedModule>(Path.Combine(hostingEnvironment.ContentRootPath, string.Format("..{0}..{0}..{0}..{0}framework{0}src{0}Aiwins.Rocket.AspNetCore.Mvc.UI.Theme.Shared", Path.DirectorySeparatorChar)));
                    options.FileSets.ReplaceEmbeddedByPhysical<RocketAspNetCoreMvcUiBasicThemeModule>(Path.Combine(hostingEnvironment.ContentRootPath, string.Format("..{0}..{0}..{0}..{0}framework{0}src{0}Aiwins.Rocket.AspNetCore.Mvc.UI.Theme.Basic", Path.DirectorySeparatorChar)));
                    options.FileSets.ReplaceEmbeddedByPhysical<DocsDomainModule>(Path.Combine(hostingEnvironment.ContentRootPath, string.Format("..{0}..{0}src{0}Aiwins.Docs.Domain", Path.DirectorySeparatorChar)));
                    options.FileSets.ReplaceEmbeddedByPhysical<DocsWebModule>(Path.Combine(hostingEnvironment.ContentRootPath, string.Format("..{0}..{0}src{0}Aiwins.Docs.Web", Path.DirectorySeparatorChar)));
                    options.FileSets.ReplaceEmbeddedByPhysical<DocsWebModule>(Path.Combine(hostingEnvironment.ContentRootPath, string.Format("..{0}..{0}src{0}Aiwins.Docs.Admin.Web", Path.DirectorySeparatorChar)));
                });
            }

            context.Services.AddSwaggerGen(
                options =>
                {
                    options.SwaggerDoc("v1", new OpenApiInfo
                    {
                        Title = "Docs API",
                        Version = "v1"
                    });
                    options.DocInclusionPredicate((docName, description) => true);
                    options.CustomSchemaIds(type => type.FullName);
                });
            
            Configure<RocketVirtualFileSystemOptions>(options =>
            {
                options.FileSets.AddEmbedded<AiwinsDocsWebModule>("AiwinsDocs.Web");
            });

            Configure<RocketLocalizationOptions>(options =>
            {
                options.Languages.Add(new LanguageInfo("cs", "cs", "Čeština"));
                options.Languages.Add(new LanguageInfo("en", "en", "English"));
                options.Languages.Add(new LanguageInfo("tr", "tr", "Türkçe"));
                options.Languages.Add(new LanguageInfo("zh-Hans", "zh-Hans", "简体中文"));

                options.Resources
                    .Get<DocsResource>()
                    .AddBaseTypes(typeof(RocketValidationResource))
                    .AddBaseTypes(typeof(RocketUiResource))
                    .AddVirtualJson("/Localization/Resources/AiwinsDocs/Web");
            });

            Configure<RocketThemingOptions>(options =>
            {
                options.DefaultThemeName = BasicTheme.Name;
            });

            Configure<RazorPagesOptions>(options =>
            {
                options.Conventions.AddPageRoute("/Error", "error/{statusCode}");
            });
        }

        public override void OnApplicationInitialization(ApplicationInitializationContext context)
        {
            var app = context.GetApplicationBuilder();
            var env = context.GetEnvironment();

            app.UseVirtualFiles();
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseRocketRequestLocalization();

            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "Support APP API");
            });

            app.UseStatusCodePagesWithReExecute("/error/{0}");
           
            //app.UseMiddleware<GlobalExceptionHandlerMiddleware>();

            app.UseConfiguredEndpoints();

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
