using System.Collections.Generic;
using Aiwins.Rocket.AspNetCore.Mvc.UI.Bundling;

namespace Aiwins.Docs.Bundling
{
    public class PrismjsStyleBundleContributorDocsExtension : BundleContributor
    {
        public override void ConfigureBundle(BundleConfigurationContext context)
        {
            context.Files.AddIfNotContains("/libs/prismjs/plugins/line-highlight/prism-line-highlight.css");
            context.Files.AddIfNotContains("/libs/prismjs/plugins/toolbar/prism-toolbar.css");
        }
    }
}