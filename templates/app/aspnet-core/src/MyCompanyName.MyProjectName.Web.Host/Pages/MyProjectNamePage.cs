using Microsoft.AspNetCore.Mvc.Localization;
using Microsoft.AspNetCore.Mvc.Razor.Internal;
using MyCompanyName.MyProjectName.Localization;
using Aiwins.Rocket.AspNetCore.Mvc.UI.RazorPages;

namespace MyCompanyName.MyProjectName.Web.Pages
{
    public abstract class MyProjectNamePage : RocketPage
    {
        [RazorInject]
        public IHtmlLocalizer<MyProjectNameResource> L { get; set; }
    }
}
