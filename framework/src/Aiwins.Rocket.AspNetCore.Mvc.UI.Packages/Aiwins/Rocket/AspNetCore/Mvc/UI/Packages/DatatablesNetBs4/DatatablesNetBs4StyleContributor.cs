using System.Collections.Generic;
using Aiwins.Rocket.AspNetCore.Mvc.UI.Bundling;
using Aiwins.Rocket.AspNetCore.Mvc.UI.Packages.Bootstrap;
using Aiwins.Rocket.Modularity;

namespace Aiwins.Rocket.AspNetCore.Mvc.UI.Packages.DatatablesNetBs4
{
    [DependsOn(typeof(BootstrapStyleContributor))]
    public class DatatablesNetBs4StyleContributor : BundleContributor
    {
        public override void ConfigureBundle(BundleConfigurationContext context)
        {
            context.Files.AddIfNotContains("/libs/datatables.net-bs4/css/dataTables.bootstrap4.css");
        }
    }
}