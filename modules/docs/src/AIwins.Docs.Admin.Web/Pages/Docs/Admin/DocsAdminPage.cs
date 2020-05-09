using Microsoft.AspNetCore.Mvc.Localization;
using Microsoft.AspNetCore.Mvc.Razor.Internal;
using Aiwins.Rocket.AspNetCore.Mvc.UI.RazorPages;
using Aiwins.Docs.Localization;

namespace Aiwins.Docs.Admin.Pages.Docs.Admin
{
    public abstract class DocsAdminPage : RocketPage
    {
        [RazorInject]
        public IHtmlLocalizer<DocsResource> L { get; set; }
    }
}
