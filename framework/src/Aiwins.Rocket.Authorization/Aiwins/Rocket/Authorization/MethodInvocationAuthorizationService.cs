using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Aiwins.Rocket.DependencyInjection;
using Microsoft.AspNetCore.Authorization;

namespace Aiwins.Rocket.Authorization {
    public class MethodInvocationAuthorizationService : IMethodInvocationAuthorizationService, ITransientDependency {
        private readonly IRocketAuthorizationPolicyProvider _abpAuthorizationPolicyProvider;
        private readonly IRocketAuthorizationService _abpAuthorizationService;

        public MethodInvocationAuthorizationService (
            IRocketAuthorizationPolicyProvider abpAuthorizationPolicyProvider,
            IRocketAuthorizationService abpAuthorizationService) {
            _abpAuthorizationPolicyProvider = abpAuthorizationPolicyProvider;
            _abpAuthorizationService = abpAuthorizationService;
        }

        public async Task CheckAsync (MethodInvocationAuthorizationContext context) {
            if (AllowAnonymous (context)) {
                return;
            }

            var authorizationPolicy = await AuthorizationPolicy.CombineAsync (
                _abpAuthorizationPolicyProvider,
                GetAuthorizationDataAttributes (context.Method)
            );

            if (authorizationPolicy == null) {
                return;
            }

            await _abpAuthorizationService.CheckAsync (authorizationPolicy);
        }

        protected virtual bool AllowAnonymous (MethodInvocationAuthorizationContext context) {
            return context.Method.GetCustomAttributes (true).OfType<IAllowAnonymous> ().Any ();
        }

        protected virtual IEnumerable<IAuthorizeData> GetAuthorizationDataAttributes (MethodInfo methodInfo) {
            var attributes = methodInfo
                .GetCustomAttributes (true)
                .OfType<IAuthorizeData> ();

            if (methodInfo.IsPublic && methodInfo.DeclaringType != null) {
                attributes = attributes
                    .Union (
                        methodInfo.DeclaringType
                        .GetCustomAttributes (true)
                        .OfType<IAuthorizeData> ()
                    );
            }

            return attributes;
        }
    }
}