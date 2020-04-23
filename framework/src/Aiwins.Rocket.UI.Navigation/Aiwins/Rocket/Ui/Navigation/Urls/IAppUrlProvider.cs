using System.Threading.Tasks;
using JetBrains.Annotations;

namespace Aiwins.Rocket.UI.Navigation.Urls
{
    public interface IAppUrlProvider
    {
        Task<string> GetUrlAsync([NotNull] string appName, [CanBeNull] string urlName = null);
    }
}
