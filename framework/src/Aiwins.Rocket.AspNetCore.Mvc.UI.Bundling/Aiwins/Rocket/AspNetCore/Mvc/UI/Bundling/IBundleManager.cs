using System.Collections.Generic;
using System.Threading.Tasks;

namespace Aiwins.Rocket.AspNetCore.Mvc.UI.Bundling
{
    public interface IBundleManager
    {
        Task<IReadOnlyList<string>> GetStyleBundleFilesAsync(string bundleName);

        Task<IReadOnlyList<string>> GetScriptBundleFilesAsync(string bundleName);
    }
}