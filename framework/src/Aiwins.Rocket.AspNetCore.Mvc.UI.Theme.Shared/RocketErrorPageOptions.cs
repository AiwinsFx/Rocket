using System.Collections.Generic;

namespace Aiwins.Rocket.AspNetCore.Mvc.UI.Theme.Shared
{
    public class RocketErrorPageOptions
    {
        public readonly IDictionary<string, string> ErrorViewUrls;

        public RocketErrorPageOptions()
        {
            ErrorViewUrls = new Dictionary<string, string>();
        }
    }
}