using System;
using System.Collections.Generic;
using System.Reflection;
using Aiwins.Rocket.DependencyInjection;
using Aiwins.Rocket.Modularity;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http.Connections;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Aiwins.Rocket.AspNetCore.SignalR {
    [DependsOn (
        typeof (RocketAspNetCoreModule)
    )]
    public class RocketAspNetCoreSignalRModule : RocketModule {
        private static readonly MethodInfo MapHubGenericMethodInfo =
            typeof (RocketAspNetCoreSignalRModule)
            .GetMethod ("MapHub", BindingFlags.Static | BindingFlags.NonPublic);

        public override void PreConfigureServices (ServiceConfigurationContext context) {
            context.Services.AddConventionalRegistrar (new RocketSignalRConventionalRegistrar ());

            AutoAddHubTypes (context.Services);
        }

        public override void ConfigureServices (ServiceConfigurationContext context) {
            context.Services.AddSignalR ();

            Configure<RocketEndpointRouterOptions> (options => {
                options.EndpointConfigureActions.Add (endpointContext => {
                    var signalROptions = endpointContext
                        .ScopeServiceProvider
                        .GetRequiredService<IOptions<RocketSignalROptions>> ()
                        .Value;

                    foreach (var hubConfig in signalROptions.Hubs) {
                        MapHubType (
                            hubConfig.HubType,
                            endpointContext.Endpoints,
                            hubConfig.RoutePattern,
                            opts => {
                                foreach (var configureAction in hubConfig.ConfigureActions) {
                                    configureAction (opts);
                                }
                            }
                        );
                    }
                });
            });
        }

        private void AutoAddHubTypes (IServiceCollection services) {
            var hubTypes = new List<Type> ();

            services.OnRegistred (context => {
                if (IsHubClass (context) && !IsDisabledForAutoMap (context)) {
                    hubTypes.Add (context.ImplementationType);
                }
            });

            services.Configure<RocketSignalROptions> (options => {
                foreach (var hubType in hubTypes) {
                    options.Hubs.Add (HubConfig.Create (hubType));
                }
            });
        }

        private static bool IsHubClass (IOnServiceRegistredContext context) {
            return typeof (Hub).IsAssignableFrom (context.ImplementationType);
        }

        private static bool IsDisabledForAutoMap (IOnServiceRegistredContext context) {
            return context.ImplementationType.IsDefined (typeof (DisableAutoHubMapAttribute), true);
        }

        private void MapHubType (
            Type hubType,
            IEndpointRouteBuilder endpoints,
            string pattern,
            Action<HttpConnectionDispatcherOptions> configureOptions) {
            MapHubGenericMethodInfo
                .MakeGenericMethod (hubType)
                .Invoke (
                    this,
                    new object[] {
                        endpoints,
                        pattern,
                        configureOptions
                    }
                );
        }

        // ReSharper disable once UnusedMember.Local (used via reflection)
        private static void MapHub<THub> (
            IEndpointRouteBuilder endpoints,
            string pattern,
            Action<HttpConnectionDispatcherOptions> configureOptions)
        where THub : Hub {
            endpoints.MapHub<THub> (
                pattern,
                configureOptions
            );
        }
    }
}