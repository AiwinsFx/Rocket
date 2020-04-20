using System;
using System.Security.Claims;
using Aiwins.Rocket.DependencyInjection;
using Aiwins.Rocket.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Aiwins.Rocket.Authorization {
    [Dependency (ReplaceServices = true)]
    public class RocketAuthorizationService : DefaultAuthorizationService, IRocketAuthorizationService, ITransientDependency {
        public IServiceProvider ServiceProvider { get; }

        public ClaimsPrincipal CurrentPrincipal => _currentPrincipalAccessor.Principal;

        private readonly ICurrentPrincipalAccessor _currentPrincipalAccessor;

        public RocketAuthorizationService (
            IAuthorizationPolicyProvider policyProvider,
            IAuthorizationHandlerProvider handlers,
            ILogger<DefaultAuthorizationService> logger,
            IAuthorizationHandlerContextFactory contextFactory,
            IAuthorizationEvaluator evaluator,
            IOptions<AuthorizationOptions> options,
            ICurrentPrincipalAccessor currentPrincipalAccessor,
            IServiceProvider serviceProvider) : base (
            policyProvider,
            handlers,
            logger,
            contextFactory,
            evaluator,
            options) {
            _currentPrincipalAccessor = currentPrincipalAccessor;
            ServiceProvider = serviceProvider;
        }
    }
}