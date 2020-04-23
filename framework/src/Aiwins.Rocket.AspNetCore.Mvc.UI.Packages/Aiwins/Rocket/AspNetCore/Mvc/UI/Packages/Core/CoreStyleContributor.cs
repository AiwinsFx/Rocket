using System.Collections.Generic;
using Aiwins.Rocket.AspNetCore.Mvc.UI.Bundling;

namespace Aiwins.Rocket.AspNetCore.Mvc.UI.Packages.Core
{
    public class CoreStyleContributor : BundleContributor
    {
        public override void ConfigureBundle(BundleConfigurationContext context)
        {
            context.Files.AddIfNotContains("/libs/abp/core/abp.css");
        }
    }
}