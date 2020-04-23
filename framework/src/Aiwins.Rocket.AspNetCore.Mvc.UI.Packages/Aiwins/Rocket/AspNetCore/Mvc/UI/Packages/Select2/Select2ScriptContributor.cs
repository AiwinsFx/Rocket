using System.Collections.Generic;
using Aiwins.Rocket.AspNetCore.Mvc.UI.Bundling;
using Aiwins.Rocket.AspNetCore.Mvc.UI.Packages.Core;
using Aiwins.Rocket.Modularity;

namespace Aiwins.Rocket.AspNetCore.Mvc.UI.Packages.Select2
{
    [DependsOn(typeof(CoreScriptContributor))]
    public class Select2ScriptContributor : BundleContributor
    {
        public override void ConfigureBundle(BundleConfigurationContext context)
        {
            //TODO: Add select2.full.min.js or localize!
            context.Files.AddIfNotContains("/libs/select2/js/select2.min.js");
            context.Files.AddIfNotContains("/libs/select2/js/select2-bootstrap-modal-patch.js");
        }
    }
}
