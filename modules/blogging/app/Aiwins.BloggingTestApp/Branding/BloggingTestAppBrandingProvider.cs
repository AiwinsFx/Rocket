using Microsoft.AspNetCore.Mvc.Localization;
using Aiwins.Rocket.AspNetCore.Mvc.UI.Theme.Shared.Components;
using Aiwins.Rocket.DependencyInjection;
using Aiwins.Blogging.Localization;

namespace Aiwins.BloggingTestApp.Branding
{
    [Dependency(ReplaceServices = true)]
    public class BloggingTestAppBrandingProvider : DefaultBrandingProvider
    {
        public IHtmlLocalizer<BloggingResource> L { get; set; }
        public BloggingTestAppBrandingProvider(IHtmlLocalizer<BloggingResource> localizer)
        {
            L = localizer;
        }
        public override string AppName => L["Blogs"].Value;
    }
}
