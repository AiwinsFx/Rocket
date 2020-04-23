using Microsoft.AspNetCore.Builder;

namespace Aiwins.Rocket.AspNetCore.Mvc.UI.Theme.Shared
{
    public static class RocketApplicationBuilderErrorPageExtensions
    {
        public static IApplicationBuilder UseErrorPage(this IApplicationBuilder app)
        {
            return app
                .UseStatusCodePagesWithRedirects("~/Error?httpStatusCode={0}")
                .UseExceptionHandler("/Error");
        }
    }
}
