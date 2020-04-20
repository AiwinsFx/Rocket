using System.Threading.Tasks;

namespace Aiwins.Rocket.Features {
    public interface IMethodInvocationFeatureCheckerService {
        Task CheckAsync (
            MethodInvocationFeatureCheckerContext context
        );
    }
}