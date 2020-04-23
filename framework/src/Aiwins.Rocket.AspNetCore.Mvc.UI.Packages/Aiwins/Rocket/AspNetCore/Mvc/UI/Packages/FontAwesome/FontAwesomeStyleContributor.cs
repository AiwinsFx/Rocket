using System.Collections.Generic;
using Aiwins.Rocket.AspNetCore.Mvc.UI.Bundling;

namespace Aiwins.Rocket.AspNetCore.Mvc.UI.Packages.FontAwesome
{
    public class FontAwesomeStyleContributor : BundleContributor
    {
        public override void ConfigureBundle(BundleConfigurationContext context)
        {
            context.Files.AddIfNotContains("/libs/@fortawesome/fontawesome-free/css/all.css");
            context.Files.AddIfNotContains("/libs/@fortawesome/fontawesome-free/css/v4-shims.css");
        }
    }
}