using Aiwins.Rocket.AspNetCore.Mvc.UI.Bundling;
using Aiwins.Rocket.AspNetCore.Mvc.UI.Packages.Bootstrap;
using Aiwins.Rocket.AspNetCore.Mvc.UI.Packages.DatatablesNetBs4;
using Aiwins.Rocket.AspNetCore.Mvc.UI.Packages.JQuery;
using Aiwins.Rocket.AspNetCore.Mvc.UI.Packages.JQueryForm;
using Aiwins.Rocket.AspNetCore.Mvc.UI.Packages.JQueryValidationUnobtrusive;
using Aiwins.Rocket.AspNetCore.Mvc.UI.Packages.Lodash;
using Aiwins.Rocket.AspNetCore.Mvc.UI.Packages.Luxon;
using Aiwins.Rocket.AspNetCore.Mvc.UI.Packages.MalihuCustomScrollbar;
using Aiwins.Rocket.AspNetCore.Mvc.UI.Packages.Select2;
using Aiwins.Rocket.AspNetCore.Mvc.UI.Packages.SweetAlert;
using Aiwins.Rocket.AspNetCore.Mvc.UI.Packages.Timeago;
using Aiwins.Rocket.AspNetCore.Mvc.UI.Packages.Toastr;
using Aiwins.Rocket.Modularity;

namespace Aiwins.Rocket.AspNetCore.Mvc.UI.Theme.Shared.Bundling
{
    [DependsOn(
        typeof(JQueryScriptContributor),
        typeof(BootstrapScriptContributor),
        typeof(LodashScriptContributor),
        typeof(JQueryValidationUnobtrusiveScriptContributor),
        typeof(JQueryFormScriptContributor),
        typeof(Select2ScriptContributor),
        typeof(DatatablesNetBs4ScriptContributor),
        typeof(SweetalertScriptContributor),
        typeof(ToastrScriptBundleContributor),
        typeof(MalihuCustomScrollbarPluginScriptBundleContributor),
        typeof(LuxonScriptContributor),
        typeof(TimeagoScriptContributor),
        typeof(BootstrapDatepickerScriptContributor)
        )]
    public class SharedThemeGlobalScriptContributor : BundleContributor
    {
        public override void ConfigureBundle(BundleConfigurationContext context)
        {
            context.Files.AddRange(new[]
            {
                "/libs/rocket/aspnetcore-mvc-ui-theme-shared/jquery/jquery-extensions.js",
                "/libs/rocket/aspnetcore-mvc-ui-theme-shared/jquery-form/jquery-form-extensions.js",
                "/libs/rocket/aspnetcore-mvc-ui-theme-shared/jquery/widget-manager.js",
                "/libs/rocket/aspnetcore-mvc-ui-theme-shared/bootstrap/dom-event-handlers.js",
                "/libs/rocket/aspnetcore-mvc-ui-theme-shared/bootstrap/modal-manager.js",
                "/libs/rocket/aspnetcore-mvc-ui-theme-shared/datatables/datatables-extensions.js",
                "/libs/rocket/aspnetcore-mvc-ui-theme-shared/sweetalert/rocket-sweetalert.js",
                "/libs/rocket/aspnetcore-mvc-ui-theme-shared/toastr/rocket-toastr.js"
            });
        }
    }
}