using System;
using System.IO;
using Localization.Resources.RocketUi;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MyCompanyName.MyProjectName.EntityFrameworkCore;
using MyCompanyName.MyProjectName.Localization;
using MyCompanyName.MyProjectName.MultiTenancy;
using MyCompanyName.MyProjectName.Web.Menus;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Swagger;
using Aiwins.Rocket;
using Aiwins.Rocket.Account.Web;
using Aiwins.Rocket.AspNetCore.Authentication.JwtBearer;
using Aiwins.Rocket.AspNetCore.Mvc;
using Aiwins.Rocket.AspNetCore.Mvc.Localization;
using Aiwins.Rocket.AspNetCore.Mvc.UI;
using Aiwins.Rocket.AspNetCore.Mvc.UI.Bootstrap;
using Aiwins.Rocket.AspNetCore.Mvc.UI.MultiTenancy;
using Aiwins.Rocket.AspNetCore.Mvc.UI.Theme.Basic;
using Aiwins.Rocket.AspNetCore.Mvc.UI.Theme.Shared;
using Aiwins.Rocket.AspNetCore.Serilog;
using Aiwins.Rocket.Autofac;
using Aiwins.Rocket.AutoMapper;
using Aiwins.Rocket.FeatureManagement;
using Aiwins.Rocket.Identity.Web;
using Aiwins.Rocket.Localization;
using Aiwins.Rocket.Modularity;
using Aiwins.Rocket.PermissionManagement.Web;
using Aiwins.Rocket.TenantManagement.Web;
using Aiwins.Rocket.UI.Navigation.Urls;
using Aiwins.Rocket.UI;
using Aiwins.Rocket.UI.Navigation;
using Aiwins.Rocket.VirtualFileSystem;

namespace MyCompanyName.MyProjectName.Web
{
    [DependsOn(
        typeof(MyProjectNameHttpApiModule),
        typeof(MyProjectNameApplicationModule),
        typeof(MyProjectNameEntityFrameworkCoreDbMigrationsModule),
        typeof(RocketAutofacModule),
        typeof(RocketIdentityWebModule),
        typeof(RocketAccountWebIdentityServerModule),
        typeof(RocketAspNetCoreMvcUiBasicThemeModule),
        typeof(RocketAspNetCoreAuthenticationJwtBearerModule),
        typeof(RocketTenantManagementWebModule),
        typeof(RocketAspNetCoreSerilogModule)
        )]
    public class MyProjectNameWebModule : RocketModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.PreConfigure<RocketMvcDataAnnotationsLocalizationOptions>(options =>
            {
                options.AddAssemblyResource(
                    typeof(MyProjectNameResource),
                    typeof(MyProjectNameDomainModule).Assembly,
                    typeof(MyProjectNameDomainSharedModule).Assembly,
                    typeof(MyProjectNameApplicationModule).Assembly,
                    typeof(MyProjectNameApplicationContractsModule).Assembly,
                    typeof(MyProjectNameWebModule).Assembly
                );
            });
        }

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            var hostingEnvironment = context.Services.GetHostingEnvironment();
            var configuration = context.Services.GetConfiguration();

            ConfigureUrls(configuration);
            ConfigureAuthentication(context, configuration);
            ConfigureAutoMapper();
            ConfigureVirtualFileSystem(hostingEnvironment);
            ConfigureLocalizationServices();
            ConfigureNavigationServices();
            ConfigureAutoApiControllers();
            ConfigureSwaggerServices(context.Services);
        }

        private void ConfigureUrls(IConfiguration configuration)
        {
            Configure<AppUrlOptions>(options =>
            {
                options.Applications["MVC"].RootUrl = configuration["App:SelfUrl"];
            });
        }

        private void ConfigureAuthentication(ServiceConfigurationContext context, IConfiguration configuration)
        {
            context.Services.AddAuthentication()
                .AddIdentityServerAuthentication(options =>
                {
                    options.Authority = configuration["AuthServer:Authority"];
                    options.RequireHttpsMetadata = false;
                    options.ApiName = "MyProjectName";
                });
        }

        private void ConfigureAutoMapper()
        {
            Configure<RocketAutoMapperOptions>(options =>
            {
                options.AddMaps<MyProjectNameWebModule>();

            });
        }

        private void ConfigureVirtualFileSystem(IWebHostEnvironment hostingEnvironment)
        {
            if (hostingEnvironment.IsDevelopment())
            {
                Configure<RocketVirtualFileSystemOptions>(options =>
                {
                    //<TEMPLATE-REMOVE>
                    options.FileSets.ReplaceEmbeddedByPhysical<RocketUiModule>(Path.Combine(hostingEnvironment.ContentRootPath, string.Format("..{0}..{0}..{0}..{0}..{0}framework{0}src{0}Aiwins.Rocket.UI", Path.DirectorySeparatorChar)));
                    options.FileSets.ReplaceEmbeddedByPhysical<RocketAspNetCoreMvcUiModule>(Path.Combine(hostingEnvironment.ContentRootPath, string.Format("..{0}..{0}..{0}..{0}..{0}framework{0}src{0}Aiwins.Rocket.AspNetCore.Mvc.UI", Path.DirectorySeparatorChar)));
                    options.FileSets.ReplaceEmbeddedByPhysical<RocketAspNetCoreMvcUiBootstrapModule>(Path.Combine(hostingEnvironment.ContentRootPath, string.Format("..{0}..{0}..{0}..{0}..{0}framework{0}src{0}Aiwins.Rocket.AspNetCore.Mvc.UI.Bootstrap", Path.DirectorySeparatorChar)));
                    options.FileSets.ReplaceEmbeddedByPhysical<RocketAspNetCoreMvcUiThemeSharedModule>(Path.Combine(hostingEnvironment.ContentRootPath, string.Format("..{0}..{0}..{0}..{0}..{0}framework{0}src{0}Aiwins.Rocket.AspNetCore.Mvc.UI.Theme.Shared", Path.DirectorySeparatorChar)));
                    options.FileSets.ReplaceEmbeddedByPhysical<RocketAspNetCoreMvcUiBasicThemeModule>(Path.Combine(hostingEnvironment.ContentRootPath, string.Format("..{0}..{0}..{0}..{0}..{0}framework{0}src{0}Aiwins.Rocket.AspNetCore.Mvc.UI.Theme.Basic", Path.DirectorySeparatorChar)));
                    options.FileSets.ReplaceEmbeddedByPhysical<RocketAspNetCoreMvcUiMultiTenancyModule>(Path.Combine(hostingEnvironment.ContentRootPath, string.Format("..{0}..{0}..{0}..{0}..{0}framework{0}src{0}Aiwins.Rocket.AspNetCore.Mvc.UI.MultiTenancy", Path.DirectorySeparatorChar)));
                    options.FileSets.ReplaceEmbeddedByPhysical<RocketPermissionManagementWebModule>(Path.Combine(hostingEnvironment.ContentRootPath, string.Format("..{0}..{0}..{0}..{0}..{0}modules{0}permission-management{0}src{0}Aiwins.Rocket.PermissionManagement.Web", Path.DirectorySeparatorChar)));
                    options.FileSets.ReplaceEmbeddedByPhysical<RocketFeatureManagementWebModule>(Path.Combine(hostingEnvironment.ContentRootPath, string.Format("..{0}..{0}..{0}..{0}..{0}modules{0}feature-management{0}src{0}Aiwins.Rocket.FeatureManagement.Web", Path.DirectorySeparatorChar)));
                    options.FileSets.ReplaceEmbeddedByPhysical<RocketIdentityWebModule>(Path.Combine(hostingEnvironment.ContentRootPath, string.Format("..{0}..{0}..{0}..{0}..{0}modules{0}identity{0}src{0}Aiwins.Rocket.Identity.Web", Path.DirectorySeparatorChar)));
                    options.FileSets.ReplaceEmbeddedByPhysical<RocketAccountWebModule>(Path.Combine(hostingEnvironment.ContentRootPath, string.Format("..{0}..{0}..{0}..{0}..{0}modules{0}account{0}src{0}Aiwins.Rocket.Account.Web", Path.DirectorySeparatorChar)));
                    options.FileSets.ReplaceEmbeddedByPhysical<RocketTenantManagementWebModule>(Path.Combine(hostingEnvironment.ContentRootPath, string.Format("..{0}..{0}..{0}..{0}..{0}modules{0}tenant-management{0}src{0}Aiwins.Rocket.TenantManagement.Web", Path.DirectorySeparatorChar)));
                    //</TEMPLATE-REMOVE>
                    options.FileSets.ReplaceEmbeddedByPhysical<MyProjectNameDomainSharedModule>(Path.Combine(hostingEnvironment.ContentRootPath, $"..{Path.DirectorySeparatorChar}MyCompanyName.MyProjectName.Domain.Shared"));
                    options.FileSets.ReplaceEmbeddedByPhysical<MyProjectNameDomainModule>(Path.Combine(hostingEnvironment.ContentRootPath, $"..{Path.DirectorySeparatorChar}MyCompanyName.MyProjectName.Domain"));
                    options.FileSets.ReplaceEmbeddedByPhysical<MyProjectNameApplicationContractsModule>(Path.Combine(hostingEnvironment.ContentRootPath, $"..{Path.DirectorySeparatorChar}MyCompanyName.MyProjectName.Application.Contracts"));
                    options.FileSets.ReplaceEmbeddedByPhysical<MyProjectNameApplicationModule>(Path.Combine(hostingEnvironment.ContentRootPath, $"..{Path.DirectorySeparatorChar}MyCompanyName.MyProjectName.Application"));
                    options.FileSets.ReplaceEmbeddedByPhysical<MyProjectNameWebModule>(hostingEnvironment.ContentRootPath);
                });
            }
        }

        private void ConfigureLocalizationServices()
        {
            Configure<RocketLocalizationOptions>(options =>
            {
                options.Resources
                    .Get<MyProjectNameResource>()
                    .AddBaseTypes(
                        typeof(RocketUiResource)
                    );

                options.Languages.Add(new LanguageInfo("cs", "cs", "Čeština"));
                options.Languages.Add(new LanguageInfo("en", "en", "English"));
                options.Languages.Add(new LanguageInfo("pt-BR", "pt-BR", "Português"));
                options.Languages.Add(new LanguageInfo("ru", "ru", "Русский"));
                options.Languages.Add(new LanguageInfo("tr", "tr", "Türkçe"));
                options.Languages.Add(new LanguageInfo("zh-Hans", "zh-Hans", "简体中文"));
                options.Languages.Add(new LanguageInfo("zh-Hant", "zh-Hant", "繁體中文"));
            });
        }

        private void ConfigureNavigationServices()
        {
            Configure<RocketNavigationOptions>(options =>
            {
                options.MenuContributors.Add(new MyProjectNameMenuContributor());
            });
        }

        private void ConfigureAutoApiControllers()
        {
            Configure<RocketAspNetCoreMvcOptions>(options =>
            {
                options.ConventionalControllers.Create(typeof(MyProjectNameApplicationModule).Assembly);
            });
        }

        private void ConfigureSwaggerServices(IServiceCollection services)
        {
            services.AddSwaggerGen(
                options =>
                {
                    options.SwaggerDoc("v1", new OpenApiInfo { Title = "MyProjectName API", Version = "v1" });
                    options.DocInclusionPredicate((docName, description) => true);
                    options.CustomSchemaIds(type => type.FullName);
                }
            );
        }

        public override void OnApplicationInitialization(ApplicationInitializationContext context)
        {
            var app = context.GetApplicationBuilder();
            var env = context.GetEnvironment();

            app.UseCorrelationId();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseErrorPage();
            }
            app.UseVirtualFiles();
            app.UseRouting();
            app.UseAuthentication();
            app.UseJwtTokenMiddleware();

            if (MultiTenancyConsts.IsEnabled)
            {
                app.UseMultiTenancy();
            }
            app.UseIdentityServer();
            app.UseAuthorization();
            app.UseRocketRequestLocalization();
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "MyProjectName API");
            });
            app.UseAuditing();
            app.UseRocketSerilogEnrichers();
            app.UseMvcWithDefaultRouteAndArea();
        }
    }
}
