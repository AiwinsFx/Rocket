using System.Threading.Tasks;
using JetBrains.Annotations;

namespace Aiwins.Rocket.Features {
    public interface IFeatureChecker {
        Task<string> GetOrNullAsync ([NotNull] string name);

        Task<bool> IsEnabledAsync (string name);
    }
}