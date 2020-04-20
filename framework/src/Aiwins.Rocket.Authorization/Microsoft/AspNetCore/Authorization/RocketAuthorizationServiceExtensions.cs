﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Aiwins.Rocket;
using Aiwins.Rocket.Authorization;

namespace Microsoft.AspNetCore.Authorization {
    public static class RocketAuthorizationServiceExtensions {
        public static async Task<AuthorizationResult> AuthorizeAsync (this IAuthorizationService authorizationService, string policyName) {
            return await AuthorizeAsync (
                authorizationService,
                null,
                policyName
            );
        }

        public static async Task<AuthorizationResult> AuthorizeAsync (this IAuthorizationService authorizationService, object resource, IAuthorizationRequirement requirement) {
            return await authorizationService.AuthorizeAsync (
                authorizationService.AsRocketAuthorizationService ().CurrentPrincipal,
                resource,
                requirement
            );
        }

        public static async Task<AuthorizationResult> AuthorizeAsync (this IAuthorizationService authorizationService, object resource, AuthorizationPolicy policy) {
            return await authorizationService.AuthorizeAsync (
                authorizationService.AsRocketAuthorizationService ().CurrentPrincipal,
                resource,
                policy
            );
        }

        public static async Task<AuthorizationResult> AuthorizeAsync (this IAuthorizationService authorizationService, AuthorizationPolicy policy) {
            return await AuthorizeAsync (
                authorizationService,
                null,
                policy
            );
        }

        public static async Task<AuthorizationResult> AuthorizeAsync (this IAuthorizationService authorizationService, object resource, IEnumerable<IAuthorizationRequirement> requirements) {
            return await authorizationService.AuthorizeAsync (
                authorizationService.AsRocketAuthorizationService ().CurrentPrincipal,
                resource,
                requirements
            );
        }

        public static async Task<AuthorizationResult> AuthorizeAsync (this IAuthorizationService authorizationService, object resource, string policyName) {
            return await authorizationService.AuthorizeAsync (
                authorizationService.AsRocketAuthorizationService ().CurrentPrincipal,
                resource,
                policyName
            );
        }

        public static async Task<bool> IsGrantedAsync (this IAuthorizationService authorizationService, string policyName) {
            return (await authorizationService.AuthorizeAsync (policyName)).Succeeded;
        }

        public static async Task<bool> IsGrantedAsync (this IAuthorizationService authorizationService, object resource, IAuthorizationRequirement requirement) {
            return (await authorizationService.AuthorizeAsync (resource, requirement)).Succeeded;
        }

        public static async Task<bool> IsGrantedAsync (this IAuthorizationService authorizationService, object resource, AuthorizationPolicy policy) {
            return (await authorizationService.AuthorizeAsync (resource, policy)).Succeeded;
        }

        public static async Task<bool> IsGrantedAsync (this IAuthorizationService authorizationService, AuthorizationPolicy policy) {
            return (await authorizationService.AuthorizeAsync (policy)).Succeeded;
        }

        public static async Task<bool> IsGrantedAsync (this IAuthorizationService authorizationService, object resource, IEnumerable<IAuthorizationRequirement> requirements) {
            return (await authorizationService.AuthorizeAsync (resource, requirements)).Succeeded;
        }

        public static async Task<bool> IsGrantedAsync (this IAuthorizationService authorizationService, object resource, string policyName) {
            return (await authorizationService.AuthorizeAsync (resource, policyName)).Succeeded;
        }

        public static async Task CheckAsync (this IAuthorizationService authorizationService, string policyName) {
            if (!await authorizationService.IsGrantedAsync (policyName)) {
                throw new RocketAuthorizationException ("Authorization failed! Given policy has not granted: " + policyName);
            }
        }

        public static async Task CheckAsync (this IAuthorizationService authorizationService, object resource, IAuthorizationRequirement requirement) {
            if (!await authorizationService.IsGrantedAsync (resource, requirement)) {
                throw new RocketAuthorizationException ("Authorization failed! Given requirement has not granted for given resource: " + resource);
            }
        }

        public static async Task CheckAsync (this IAuthorizationService authorizationService, object resource, AuthorizationPolicy policy) {
            if (!await authorizationService.IsGrantedAsync (resource, policy)) {
                throw new RocketAuthorizationException ("Authorization failed! Given policy has not granted for given resource: " + resource);
            }
        }

        public static async Task CheckAsync (this IAuthorizationService authorizationService, AuthorizationPolicy policy) {
            if (!await authorizationService.IsGrantedAsync (policy)) {
                throw new RocketAuthorizationException ("Authorization failed! Given policy has not granted.");
            }
        }

        public static async Task CheckAsync (this IAuthorizationService authorizationService, object resource, IEnumerable<IAuthorizationRequirement> requirements) {
            if (!await authorizationService.IsGrantedAsync (resource, requirements)) {
                throw new RocketAuthorizationException ("Authorization failed! Given requirements have not granted for given resource: " + resource);
            }
        }

        public static async Task CheckAsync (this IAuthorizationService authorizationService, object resource, string policyName) {
            if (!await authorizationService.IsGrantedAsync (resource, policyName)) {
                throw new RocketAuthorizationException ("Authorization failed! Given polist has not granted for given resource: " + resource);
            }
        }

        private static IRocketAuthorizationService AsRocketAuthorizationService (this IAuthorizationService authorizationService) {
            if (!(authorizationService is IRocketAuthorizationService abpAuthorizationService)) {
                throw new RocketException ($"{nameof(authorizationService)} should implement {typeof(IRocketAuthorizationService).FullName}");
            }

            return abpAuthorizationService;
        }
    }
}