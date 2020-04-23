using System;
using System.Collections.Generic;
using System.Text;
using Aiwins.Rocket.AspNetCore.Mvc.UI.Bundling;

namespace Aiwins.Rocket.AspNetCore.Mvc.UI.Packages.Luxon
{
    public class LuxonScriptContributor : BundleContributor
    {
        public override void ConfigureBundle(BundleConfigurationContext context)
        {
            context.Files.AddIfNotContains("/libs/luxon/luxon.min.js");
        }
    }
}
