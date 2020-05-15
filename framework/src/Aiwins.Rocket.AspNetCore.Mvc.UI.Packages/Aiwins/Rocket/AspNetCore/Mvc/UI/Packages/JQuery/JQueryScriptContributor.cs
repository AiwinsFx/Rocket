using System.Collections.Generic;
using Aiwins.Rocket.AspNetCore.Mvc.UI.Bundling;
using Aiwins.Rocket.AspNetCore.Mvc.UI.Packages.Core;
using Aiwins.Rocket.Modularity;

namespace Aiwins.Rocket.AspNetCore.Mvc.UI.Packages.JQuery
{
    [DependsOn(typeof(CoreScriptContributor))]
    public class JQueryScriptContributor : BundleContributor
    {
        public override void ConfigureBundle(BundleConfigurationContext context)
        {
            context.Files.AddIfNotContains("/libs/jquery/jquery.js");
            context.Files.AddIfNotContains("/libs/aiwins/jquery/rocket.jquery.js");
        }
    }
}
