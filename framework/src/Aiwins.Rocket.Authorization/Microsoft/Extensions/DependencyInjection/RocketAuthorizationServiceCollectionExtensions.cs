using Aiwins.Rocket.Authorization;
using Aiwins.Rocket.Authorization.Permissions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Microsoft.Extensions.DependencyInjection {
    public static class RocketAuthorizationServiceCollectionExtensions {
        public static IServiceCollection AddAlwaysAllowAuthorization (this IServiceCollection services) {
            services.Replace (ServiceDescriptor.Singleton<IAuthorizationService, AlwaysAllowAuthorizationService> ());
            services.Replace (ServiceDescriptor.Singleton<IRocketAuthorizationService, AlwaysAllowAuthorizationService> ());
            services.Replace (ServiceDescriptor.Singleton<IMethodInvocationAuthorizationService, AlwaysAllowMethodInvocationAuthorizationService> ());
            return services.Replace (ServiceDescriptor.Singleton<IPermissionChecker, AlwaysAllowPermissionChecker> ());
        }
    }
}