using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Aiwins.Rocket.AspNetCore.Mvc.UI.Bundling;

namespace Aiwins.Blogging.Bundling
{
    public class PrismjsStyleBundleContributorBloggingExtension : BundleContributor
    {
        public override void ConfigureBundle(BundleConfigurationContext context)
        {
            context.Files.AddIfNotContains("/libs/prismjs/plugins/line-highlight/prism-line-highlight.css");
            context.Files.AddIfNotContains("/libs/prismjs/plugins/toolbar/prism-toolbar.css");
        }
    }
}
