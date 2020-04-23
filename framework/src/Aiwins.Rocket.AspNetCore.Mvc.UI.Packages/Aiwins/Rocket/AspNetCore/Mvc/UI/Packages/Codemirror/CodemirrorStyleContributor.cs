using System.Collections.Generic;
using Aiwins.Rocket.AspNetCore.Mvc.UI.Bundling;

namespace Aiwins.Rocket.AspNetCore.Mvc.UI.Packages.Codemirror
{
    public class CodemirrorStyleContributor : BundleContributor
    {
        public override void ConfigureBundle(BundleConfigurationContext context)
        {
            context.Files.AddIfNotContains("/libs/codemirror/codemirror.css");
        }
    }
}