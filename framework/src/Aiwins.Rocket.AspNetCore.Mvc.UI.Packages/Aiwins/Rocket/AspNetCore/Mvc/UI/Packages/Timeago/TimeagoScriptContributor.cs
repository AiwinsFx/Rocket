using System.Collections.Generic;
using System.Globalization;
using Aiwins.Rocket.AspNetCore.Mvc.UI.Bundling;
using Aiwins.Rocket.AspNetCore.Mvc.UI.Packages.JQuery;
using Aiwins.Rocket.Modularity;

namespace Aiwins.Rocket.AspNetCore.Mvc.UI.Packages.Timeago
{
    [DependsOn(typeof(JQueryScriptContributor))]
    public class TimeagoScriptContributor : BundleContributor
    {
        public override void ConfigureBundle(BundleConfigurationContext context)
        {
            context.Files.AddIfNotContains("/libs/timeago/jquery.timeago.js");
        }

        public override void ConfigureDynamicResources(BundleConfigurationContext context)
        {
            var cultureName = CultureInfo.CurrentUICulture.TwoLetterISOLanguageName;
            if (cultureName.StartsWith("en"))
            {
                return;
            }

            var cultureFileName = $"/libs/timeago/locales/jquery.timeago.{cultureName}.js";

            if (context.FileProvider.GetFileInfo(cultureFileName).Exists)
            {
                context.Files.Add(cultureFileName);
            }
        }
    }
}
