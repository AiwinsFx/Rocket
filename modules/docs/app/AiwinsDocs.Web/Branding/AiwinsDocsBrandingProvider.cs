using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Localization;
using Aiwins.Rocket.AspNetCore.Mvc.UI.Theme.Shared.Components;
using Aiwins.Rocket.DependencyInjection;
using Aiwins.Docs.Localization;

namespace AiwinsDocs.Web.Branding
{
    [Dependency(ReplaceServices = true)]
    public class AiwinsDocsBrandingProvider : DefaultBrandingProvider
    {
        public AiwinsDocsBrandingProvider(IConfiguration configuration, IStringLocalizer<DocsResource> localizer)
        {
            AppName = localizer["DocsTitle"];

            if (configuration["LogoUrl"] != null)
            {
                LogoUrl = configuration["LogoUrl"];
            }
        }

        public override string AppName { get; }

        public override string LogoUrl { get; }
    }
}
