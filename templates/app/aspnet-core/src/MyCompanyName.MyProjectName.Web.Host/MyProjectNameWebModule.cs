using System;
using System.IO;
using Microsoft.AspNetCore.Authentication.OAuth.Claims;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using MyCompanyName.MyProjectName.Localization;
using MyCompanyName.MyProjectName.MultiTenancy;
using MyCompanyName.MyProjectName.Web.Menus;
using StackExchange.Redis;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Swagger;
using Aiwins.Rocket;
using Aiwins.Rocket.AspNetCore.Authentication.OAuth;
using Aiwins.Rocket.AspNetCore.Mvc.Client;
using Aiwins.Rocket.AspNetCore.Mvc.Localization;
using Aiwins.Rocket.AspNetCore.Mvc.UI;
using Aiwins.Rocket.AspNetCore.Mvc.UI.Bootstrap;
using Aiwins.Rocket.AspNetCore.Mvc.UI.Theme.Basic;
using Aiwins.Rocket.AspNetCore.Mvc.UI.Theme.Shared;
using Aiwins.Rocket.AspNetCore.Serilog;
using Aiwins.Rocket.Autofac;
using Aiwins.Rocket.AutoMapper;
using Aiwins.Rocket.Caching;
using Aiwins.Rocket.FeatureManagement;
using Aiwins.Rocket.Http.Client.IdentityModel.Web;
using Aiwins.Rocket.Identity.Web;
using Aiwins.Rocket.Modularity;
using Aiwins.Rocket.MultiTenancy;
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
        typeof(MyProjectNameHttpApiClientModule),
        typeof(RocketAspNetCoreAuthenticationOAuthModule),
        typeof(RocketAspNetCoreMvcClientModule),
        typeof(RocketAspNetCoreMvcUiBasicThemeModule),
        typeof(RocketAutofacModule),
        typeof(RocketFeatureManagementWebModule),
        typeof(RocketHttpClientIdentityModelWebModule),
        typeof(RocketIdentityWebModule),
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
                    typeof(MyProjectNameDomainSharedModule).Assembly,
                    typeof(MyProjectNameApplicationContractsModule).Assembly,
                    typeof(MyProjectNameWebModule).Assembly
                );
            });
        }
        
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            var hostingEnvironment = context.Services.GetHostingEnvironment();
            var configuration = context.Services.GetConfiguration();

            ConfigureCache(configuration);
            ConfigureUrls(configuration);
            ConfigureAuthentication(context, configuration);
            ConfigureAutoMapper();
            ConfigureVirtualFileSystem(hostingEnvironment);
            ConfigureNavigationServices(configuration);
            ConfigureMultiTenancy();
            ConfigureSwaggerServices(context.Services);
        }

        private void ConfigureCache(IConfiguration configuration)
        {
            Configure<RocketDistributedCacheOptions>(options =>
            {
                options.KeyPrefix = "MyProjectName:";
            });
        }

        private void ConfigureUrls(IConfiguration configuration)
        {
            Configure<AppUrlOptions>(options =>
            {
                options.Applications["MVC"].RootUrl = configuration["App:SelfUrl"];
            });
        }

        private void ConfigureMultiTenancy()
        {
            Configure<RocketMultiTenancyOptions>(options =>
            {
                options.IsEnabled = MultiTenancyConsts.IsEnabled;
            });
        }

        private void ConfigureAuthentication(ServiceConfigurationContext context, IConfiguration configuration)
        {
            context.Services.AddAuthentication(options =>
                {
                    options.DefaultScheme = "Cookies";
                    options.DefaultChallengeScheme = "oidc";
                })
                .AddCookie("Cookies", options =>
                {
                    options.ExpireTimeSpan = TimeSpan.FromDays(365);
                })
                .AddOpenIdConnect("oidc", options =>
                {
                    options.Authority = configuration["AuthServer:Authority"];
                    options.RequireHttpsMetadata = true;
                    options.ResponseType = OpenIdConnectResponseType.CodeIdToken;

                    options.ClientId = configuration["AuthServer:ClientId"];
                    options.ClientSecret = configuration["AuthServer:ClientSecret"];

                    options.SaveTokens = true;
                    options.GetClaimsFromUserInfoEndpoint = true;

                    options.Scope.Add("role");
                    options.Scope.Add("email");
                    options.Scope.Add("phone");
                    options.Scope.Add("MyProjectName");

                    options.ClaimActions.MapRocketClaimTypes();
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
                    options.FileSets.ReplaceEmbeddedByPhysical<RocketPermissionManagementWebModule>(Path.Combine(hostingEnvironment.ContentRootPath, string.Format("..{0}..{0}..{0}..{0}..{0}modules{0}permission-management{0}src{0}Aiwins.Rocket.PermissionManagement.Web", Path.DirectorySeparatorChar)));
                    options.FileSets.ReplaceEmbeddedByPhysical<RocketIdentityWebModule>(Path.Combine(hostingEnvironment.ContentRootPath, string.Format("..{0}..{0}..{0}..{0}..{0}modules{0}identity{0}src{0}Aiwins.Rocket.Identity.Web", Path.DirectorySeparatorChar)));
                    //</TEMPLATE-REMOVE>
                    options.FileSets.ReplaceEmbeddedByPhysical<MyProjectNameDomainSharedModule>(Path.Combine(hostingEnvironment.ContentRootPath, $"..{Path.DirectorySeparatorChar}MyCompanyName.MyProjectName.Domain"));
                    options.FileSets.ReplaceEmbeddedByPhysical<MyProjectNameApplicationContractsModule>(Path.Combine(hostingEnvironment.ContentRootPath, $"..{Path.DirectorySeparatorChar}MyCompanyName.MyProjectName.Application.Contracts"));
                    options.FileSets.ReplaceEmbeddedByPhysical<MyProjectNameWebModule>(hostingEnvironment.ContentRootPath);
                });
            }
        }

        private void ConfigureNavigationServices(IConfiguration configuration)
        {
            Configure<RocketNavigationOptions>(options =>
            {
                options.MenuContributors.Add(new MyProjectNameMenuContributor(configuration));
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

        private void ConfigureRedis(
            ServiceConfigurationContext context,
            IConfiguration configuration,
            IWebHostEnvironment hostingEnvironment)
        {
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

            if (MultiTenancyConsts.IsEnabled)
            {
                app.UseMultiTenancy();
            }

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
