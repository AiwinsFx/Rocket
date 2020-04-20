using System.Threading.Tasks;

namespace Aiwins.Rocket.Authorization {
    public class AlwaysAllowMethodInvocationAuthorizationService : IMethodInvocationAuthorizationService {
        public Task CheckAsync (MethodInvocationAuthorizationContext context) {
            return Task.CompletedTask;
        }
    }
}