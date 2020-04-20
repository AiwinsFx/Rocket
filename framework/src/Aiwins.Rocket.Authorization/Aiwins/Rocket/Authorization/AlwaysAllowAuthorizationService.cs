using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Aiwins.Rocket.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace Aiwins.Rocket.Authorization {
    public class AlwaysAllowAuthorizationService : IRocketAuthorizationService {
        public IServiceProvider ServiceProvider { get; }

        public ClaimsPrincipal CurrentPrincipal => _currentPrincipalAccessor.Principal;

        private readonly ICurrentPrincipalAccessor _currentPrincipalAccessor;

        public AlwaysAllowAuthorizationService (IServiceProvider serviceProvider, ICurrentPrincipalAccessor currentPrincipalAccessor) {
            ServiceProvider = serviceProvider;
            _currentPrincipalAccessor = currentPrincipalAccessor;
        }

        public Task<AuthorizationResult> AuthorizeAsync (ClaimsPrincipal user, object resource, IEnumerable<IAuthorizationRequirement> requirements) {
            return Task.FromResult (AuthorizationResult.Success ());
        }

        public Task<AuthorizationResult> AuthorizeAsync (ClaimsPrincipal user, object resource, string policyName) {
            return Task.FromResult (AuthorizationResult.Success ());
        }
    }
}