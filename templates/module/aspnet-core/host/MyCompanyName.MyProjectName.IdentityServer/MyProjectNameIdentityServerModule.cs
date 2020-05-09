using System;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MyCompanyName.MyProjectName.MultiTenancy;
using StackExchange.Redis;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Swagger;
using Aiwins.Rocket;
using Aiwins.Rocket.Account;
using Aiwins.Rocket.Account.Web;
using Aiwins.Rocket.AspNetCore.Authentication.JwtBearer;
using Aiwins.Rocket.AspNetCore.Mvc;
using Aiwins.Rocket.AspNetCore.Mvc.UI.MultiTenancy;
using Aiwins.Rocket.AspNetCore.Mvc.UI.Theme.Basic;
using Aiwins.Rocket.AspNetCore.Serilog;
using Aiwins.Rocket.Auditing;
using Aiwins.Rocket.AuditLogging.EntityFrameworkCore;
using Aiwins.Rocket.Autofac;
using Aiwins.Rocket.Caching;
using Aiwins.Rocket.Data;
using Aiwins.Rocket.EntityFrameworkCore;
using Aiwins.Rocket.EntityFrameworkCore.SqlServer;
using Aiwins.Rocket.FeatureManagement;
using Aiwins.Rocket.Identity;
using Aiwins.Rocket.Identity.EntityFrameworkCore;
using Aiwins.Rocket.IdentityServer.EntityFrameworkCore;
using Aiwins.Rocket.Localization;
using Aiwins.Rocket.Modularity;
using Aiwins.Rocket.MultiTenancy;
using Aiwins.Rocket.PermissionManagement;
using Aiwins.Rocket.PermissionManagement.EntityFrameworkCore;
using Aiwins.Rocket.PermissionManagement.HttpApi;
using Aiwins.Rocket.PermissionManagement.Identity;
using Aiwins.Rocket.SettingManagement.EntityFrameworkCore;
using Aiwins.Rocket.TenantManagement;
using Aiwins.Rocket.TenantManagement.EntityFrameworkCore;
using Aiwins.Rocket.Threading;
using Aiwins.Rocket.UI.Navigation.Urls;

namespace MyCompanyName.MyProjectName
{
    [DependsOn(
        typeof(RocketAccountWebIdentityServerModule),
        typeof(RocketAccountApplicationModule),
        typeof(RocketAspNetCoreMvcUiMultiTenancyModule),
        typeof(RocketAspNetCoreMvcModule),
        typeof(RocketAspNetCoreMvcUiBasicThemeModule),
        typeof(RocketAuditLoggingEntityFrameworkCoreModule),
        typeof(RocketAutofacModule),
        typeof(RocketEntityFrameworkCoreSqlServerModule),
        typeof(RocketIdentityEntityFrameworkCoreModule),
        typeof(RocketIdentityApplicationModule),
        typeof(RocketIdentityHttpApiModule),
        typeof(RocketIdentityServerEntityFrameworkCoreModule),
        typeof(RocketPermissionManagementDomainIdentityModule),
        typeof(RocketPermissionManagementEntityFrameworkCoreModule),
        typeof(RocketPermissionManagementApplicationModule),
        typeof(RocketPermissionManagementHttpApiModule),
        typeof(RocketSettingManagementEntityFrameworkCoreModule),
        typeof(RocketFeatureManagementApplicationModule),
        typeof(RocketTenantManagementEntityFrameworkCoreModule),
        typeof(RocketTenantManagementApplicationModule),
        typeof(RocketTenantManagementHttpApiModule),
        typeof(RocketAspNetCoreAuthenticationJwtBearerModule),
        typeof(MyProjectNameApplicationContractsModule),
        typeof(RocketAspNetCoreSerilogModule)
        )]
    public class MyProjectNameIdentityServerModule : RocketModule
    {
        private const string DefaultCorsPolicyName = "Default";

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            var hostingEnvironment = context.Services.GetHostingEnvironment();
            var configuration = context.Services.GetConfiguration();

            Configure<RocketDbContextOptions>(options =>
            {
                options.UseSqlServer();
            });

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
                options.Languages.Add(new LanguageInfo("pt-BR", "pt-BR", "Português"));
                options.Languages.Add(new LanguageInfo("ru", "ru", "Русский"));
                options.Languages.Add(new LanguageInfo("tr", "tr", "Türkçe"));
                options.Languages.Add(new LanguageInfo("zh-Hans", "zh-Hans", "简体中文"));
                options.Languages.Add(new LanguageInfo("zh-Hant", "zh-Hant", "繁體中文"));
            });

            Configure<RocketAuditingOptions>(options =>
            {
                //options.IsEnabledForGetRequests = true;
                options.ApplicationName = "AuthServer";
            });

            Configure<AppUrlOptions>(options =>
            {
                options.Applications["MVC"].RootUrl = configuration["App:SelfUrl"];
            });

            context.Services.AddAuthentication()
                .AddIdentityServerAuthentication(options =>
                {
                    options.Authority = configuration["AuthServer:Authority"];
                    options.RequireHttpsMetadata = false;
                    options.ApiName = configuration["AuthServer:ApiName"];
                });

            Configure<RocketDistributedCacheOptions>(options =>
            {
                options.KeyPrefix = "MyProjectName:";
            });

            Configure<RocketMultiTenancyOptions>(options =>
            {
                options.IsEnabled = MultiTenancyConsts.IsEnabled;
            });

            context.Services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = configuration["Redis:Configuration"];
            });

            if (!hostingEnvironment.IsDevelopment())
            {
                var redis = ConnectionMultiplexer.Connect(configuration["Redis:Configuration"]);
                context.Services
                    .AddDataProtection()
                    .PersistKeysToStackExchangeRedis(redis, "MyProjectName-Protection-Keys");
            }

            context.Services.AddCors(options =>
            {
                options.AddPolicy(DefaultCorsPolicyName, builder =>
                {
                    builder
                        .WithOrigins(
                            configuration["App:CorsOrigins"]
                                .Split(",", StringSplitOptions.RemoveEmptyEntries)
                                .Select(o => o.RemovePostFix("/"))
                                .ToArray()
                        )
                        .WithRocketExposedHeaders()
                        .SetIsOriginAllowedToAllowWildcardSubdomains()
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials();
                });
            });
        }

        public override void OnApplicationInitialization(ApplicationInitializationContext context)
        {
            var app = context.GetApplicationBuilder();

            if (!context.GetEnvironment().IsDevelopment())
            {
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseCorrelationId();
            app.UseVirtualFiles();
            app.UseRouting();
            app.UseCors(DefaultCorsPolicyName);
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
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "Support APP API");
            });
            app.UseAuditing();
            app.UseRocketSerilogEnrichers();
            app.UseMvcWithDefaultRouteAndArea();

            SeedData(context);
        }

        private void SeedData(ApplicationInitializationContext context)
        {
            AsyncHelper.RunSync(async () =>
            {
                using (var scope = context.ServiceProvider.CreateScope())
                {
                    await scope.ServiceProvider
                        .GetRequiredService<IDataSeeder>()
                        .SeedAsync();
                }
            });
        }
    }
}
