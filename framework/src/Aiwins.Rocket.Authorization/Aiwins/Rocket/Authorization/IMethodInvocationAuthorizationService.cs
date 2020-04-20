using System.Threading.Tasks;

namespace Aiwins.Rocket.Authorization {
    public interface IMethodInvocationAuthorizationService {
        Task CheckAsync (MethodInvocationAuthorizationContext context);
    }
}