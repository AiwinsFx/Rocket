using System.Collections.Generic;

namespace Aiwins.Rocket.AspNetCore.Mvc.UI.Resources
{
    public interface IWebRequestResources
    {
        List<string> TryAdd(IEnumerable<string> resources);
    }
}
