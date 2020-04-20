using System.Reflection;

namespace Aiwins.Rocket.Authorization {
    public class MethodInvocationAuthorizationContext {
        public MethodInfo Method { get; }

        public MethodInvocationAuthorizationContext (MethodInfo method) {
            Method = method;
        }
    }
}