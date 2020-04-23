using System.Collections.Generic;
using Aiwins.Rocket.AspNetCore.Mvc.UI.Bundling;
using Aiwins.Rocket.AspNetCore.Mvc.UI.Packages.JQueryValidation;
using Aiwins.Rocket.Modularity;

namespace Aiwins.Rocket.AspNetCore.Mvc.UI.Packages.JQueryValidationUnobtrusive
{
    [DependsOn(typeof(JQueryValidationScriptContributor))]
    public class JQueryValidationUnobtrusiveScriptContributor : BundleContributor
    {
        public override void ConfigureBundle(BundleConfigurationContext context)
        {
            context.Files.AddIfNotContains("/libs/jquery-validation-unobtrusive/jquery.validate.unobtrusive.js");
        }
    }
}
