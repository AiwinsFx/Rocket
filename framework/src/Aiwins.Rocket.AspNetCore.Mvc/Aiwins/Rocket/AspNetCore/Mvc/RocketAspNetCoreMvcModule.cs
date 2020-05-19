using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using Aiwins.Rocket.ApiVersioning;
using Aiwins.Rocket.AspNetCore.Mvc.ApiExploring;
using Aiwins.Rocket.AspNetCore.Mvc.Conventions;
using Aiwins.Rocket.AspNetCore.Mvc.DependencyInjection;
using Aiwins.Rocket.AspNetCore.Mvc.Json;
using Aiwins.Rocket.AspNetCore.Mvc.Localization;
using Aiwins.Rocket.AspNetCore.VirtualFileSystem;
using Aiwins.Rocket.Caching;
using Aiwins.Rocket.DependencyInjection;
using Aiwins.Rocket.DynamicProxy;
using Aiwins.Rocket.Http;
using Aiwins.Rocket.Http.Modeling;
using Aiwins.Rocket.Localization;
using Aiwins.Rocket.Modularity;
using Aiwins.Rocket.UI;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.Razor.RuntimeCompilation;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.RazorPages.Infrastructure;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;

namespace Aiwins.Rocket.AspNetCore.Mvc {
    [DependsOn (
        typeof (RocketAspNetCoreModule),
        typeof (RocketLocalizationModule),
        typeof (RocketCachingModule),
        typeof (RocketApiVersioningAbstractionsModule),
        typeof (RocketAspNetCoreMvcContractsModule),
        typeof (RocketUiModule)
    )]

    public class RocketAspNetCoreMvcModule : RocketModule {
        public override void PreConfigureServices (ServiceConfigurationContext context) {
            DynamicProxyIgnoreTypes.Add<ControllerBase> ();
            DynamicProxyIgnoreTypes.Add<PageModel> ();

            context.Services.AddConventionalRegistrar (new RocketAspNetCoreMvcConventionalRegistrar ());
        }

        public override void ConfigureServices (ServiceConfigurationContext context) {
            Configure<RocketApiDescriptionModelOptions> (options => {
                options.IgnoredInterfaces.AddIfNotContains (typeof (IAsyncActionFilter));
                options.IgnoredInterfaces.AddIfNotContains (typeof (IFilterMetadata));
                options.IgnoredInterfaces.AddIfNotContains (typeof (IActionFilter));
            });

            Configure<RocketRemoteServiceApiDescriptionProviderOptions> (options => {
                var statusCodes = new List<int> {
                (int) HttpStatusCode.Forbidden,
                (int) HttpStatusCode.Unauthorized,
                (int) HttpStatusCode.BadRequest,
                (int) HttpStatusCode.NotFound,
                (int) HttpStatusCode.NotImplemented,
                (int) HttpStatusCode.InternalServerError
                };

                options.SupportedResponseTypes.AddIfNotContains (statusCodes.Select (statusCode => new ApiResponseType {
                    Type = typeof (RemoteServiceErrorResponse),
                        StatusCode = statusCode
                }));
            });

            context.Services.PostConfigure<RocketAspNetCoreMvcOptions> (options => {
                if (options.MinifyGeneratedScript == null) {
                    options.MinifyGeneratedScript = context.Services.GetHostingEnvironment ().IsProduction ();
                }
            });

            var mvcCoreBuilder = context.Services.AddMvcCore ();
            context.Services.ExecutePreConfiguredActions (mvcCoreBuilder);

            var rocketMvcDataAnnotationsLocalizationOptions = context.Services.ExecutePreConfiguredActions (new RocketMvcDataAnnotationsLocalizationOptions ());

            context.Services
                .AddSingleton<IOptions<RocketMvcDataAnnotationsLocalizationOptions>> (
                    new OptionsWrapper<RocketMvcDataAnnotationsLocalizationOptions> (
                        rocketMvcDataAnnotationsLocalizationOptions
                    )
                );

            var mvcBuilder = context.Services.AddMvc ()
                .AddNewtonsoftJson (options => {
                    options.SerializerSettings.ContractResolver =
                        new RocketMvcJsonContractResolver (context.Services);
                })
                .AddRazorRuntimeCompilation ()
                .AddDataAnnotationsLocalization (options => {
                    options.DataAnnotationLocalizerProvider = (type, factory) => {
                        var resourceType = rocketMvcDataAnnotationsLocalizationOptions
                            .AssemblyResources
                            .GetOrDefault (type.Assembly);

                        if (resourceType != null) {
                            return factory.Create (resourceType);
                        }

                        return factory.CreateDefaultOrNull () ??
                            factory.Create (type);
                    };
                })
                .AddViewLocalization (); //TODO: 如何做到应用程序灵活配置？另外，考虑迁移到UI模块

            Configure<MvcRazorRuntimeCompilationOptions> (options => {
                options.FileProviders.Add (
                    new RazorViewEngineVirtualFileProvider (
                        context.Services.GetSingletonInstance<IObjectAccessor<IServiceProvider>> ()
                    )
                );
            });

            context.Services.ExecutePreConfiguredActions (mvcBuilder);

            //TODO: 考虑默认情况下添加AddViewLocalization?

            context.Services.TryAddSingleton<IActionContextAccessor, ActionContextAccessor> ();

            // 通过依赖注入创建控制器
            context.Services.Replace (ServiceDescriptor.Transient<IControllerActivator, ServiceBasedControllerActivator> ());

            // 通过DI创建视图组件
            context.Services.Replace (ServiceDescriptor.Singleton<IViewComponentActivator, ServiceBasedViewComponentActivator> ());

            // 通过DI创建Razor页面
            context.Services.Replace (ServiceDescriptor.Singleton<IPageModelActivatorProvider, ServiceBasedPageModelActivatorProvider> ());

            // 添加特性功能提供商
            var partManager = context.Services.GetSingletonInstance<ApplicationPartManager> ();
            var application = context.Services.GetSingletonInstance<IRocketApplication> ();

            partManager.FeatureProviders.Add (new RocketConventionalControllerFeatureProvider (application));
            partManager.ApplicationParts.Add (new AssemblyPart (typeof (RocketAspNetCoreMvcModule).Assembly));

            Configure<MvcOptions> (mvcOptions => {
                mvcOptions.AddRocket (context.Services);
            });

            Configure<RocketEndpointRouterOptions> (options => {
                options.EndpointConfigureActions.Add (context => {
                    context.Endpoints.MapControllerRoute ("defaultWithArea", "{area}/{controller=Home}/{action=Index}/{id?}");
                    context.Endpoints.MapControllerRoute ("default", "{controller=Home}/{action=Index}/{id?}");
                    context.Endpoints.MapRazorPages ();
                });
            });
        }

        public override void OnApplicationInitialization (ApplicationInitializationContext context) {
            AddApplicationParts (context);
        }

        private static void AddApplicationParts (ApplicationInitializationContext context) {
            var partManager = context.ServiceProvider.GetService<ApplicationPartManager> ();
            if (partManager == null) {
                return;
            }

            // 插件模块
            var moduleAssemblies = context
                .ServiceProvider
                .GetRequiredService<IModuleContainer> ()
                .Modules
                .Where (m => m.IsLoadedAsPlugIn)
                .Select (m => m.Type.Assembly)
                .Distinct ();

            AddToApplicationParts (partManager, moduleAssemblies);

            // 应用程序服务的Controller
            var controllerAssemblies = context
                .ServiceProvider
                .GetRequiredService<IOptions<RocketAspNetCoreMvcOptions>> ()
                .Value
                .ConventionalControllers
                .ConventionalControllerSettings
                .Select (s => s.Assembly)
                .Distinct ();

            AddToApplicationParts (partManager, controllerAssemblies);
        }

        private static void AddToApplicationParts (ApplicationPartManager partManager, IEnumerable<Assembly> moduleAssemblies) {
            foreach (var moduleAssembly in moduleAssemblies) {
                if (partManager.ApplicationParts.OfType<AssemblyPart> ().Any (p => p.Assembly == moduleAssembly)) {
                    continue;
                }

                partManager.ApplicationParts.Add (new AssemblyPart (moduleAssembly));
            }
        }
    }
}