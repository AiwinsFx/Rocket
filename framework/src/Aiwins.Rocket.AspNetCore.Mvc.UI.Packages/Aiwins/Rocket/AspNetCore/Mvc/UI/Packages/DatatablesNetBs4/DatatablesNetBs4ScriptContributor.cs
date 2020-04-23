using System.Collections.Generic;
using Aiwins.Rocket.AspNetCore.Mvc.UI.Bundling;
using Aiwins.Rocket.AspNetCore.Mvc.UI.Packages.Bootstrap;
using Aiwins.Rocket.AspNetCore.Mvc.UI.Packages.DatatablesNet;
using Aiwins.Rocket.Modularity;

namespace Aiwins.Rocket.AspNetCore.Mvc.UI.Packages.DatatablesNetBs4
{
    [DependsOn(typeof(DatatablesNetScriptContributor))]
    [DependsOn(typeof(BootstrapScriptContributor))]
    public class DatatablesNetBs4ScriptContributor : BundleContributor
    {
        public override void ConfigureBundle(BundleConfigurationContext context)
        {
            context.Files.AddIfNotContains("/libs/datatables.net-bs4/js/dataTables.bootstrap4.js");
        }
    }
}