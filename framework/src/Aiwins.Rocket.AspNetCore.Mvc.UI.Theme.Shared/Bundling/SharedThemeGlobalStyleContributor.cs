using Aiwins.Rocket.AspNetCore.Mvc.UI.Bundling;
using Aiwins.Rocket.AspNetCore.Mvc.UI.Packages.Bootstrap;
using Aiwins.Rocket.AspNetCore.Mvc.UI.Packages.Core;
using Aiwins.Rocket.AspNetCore.Mvc.UI.Packages.DatatablesNetBs4;
using Aiwins.Rocket.AspNetCore.Mvc.UI.Packages.FontAwesome;
using Aiwins.Rocket.AspNetCore.Mvc.UI.Packages.MalihuCustomScrollbar;
using Aiwins.Rocket.AspNetCore.Mvc.UI.Packages.Select2;
using Aiwins.Rocket.AspNetCore.Mvc.UI.Packages.Toastr;
using Aiwins.Rocket.Modularity;

namespace Aiwins.Rocket.AspNetCore.Mvc.UI.Theme.Shared.Bundling
{
    [DependsOn(
        typeof(CoreStyleContributor),
        typeof(BootstrapStyleContributor),
        typeof(FontAwesomeStyleContributor),
        typeof(ToastrStyleBundleContributor),
        typeof(Select2StyleContributor),
        typeof(MalihuCustomScrollbarPluginStyleBundleContributor),
        typeof(DatatablesNetBs4StyleContributor),
        typeof(BootstrapDatepickerStyleContributor)
    )]
    public class SharedThemeGlobalStyleContributor : BundleContributor
    {
        public override void ConfigureBundle(BundleConfigurationContext context)
        {
            context.Files.AddRange(new[]
            {
                "/libs/aiwins/aspnetcore-mvc-ui-theme-shared/datatables/datatables-styles.css"
            });
        }
    }
}