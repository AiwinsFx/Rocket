using Aiwins.Rocket.AspNetCore.Mvc.UI.Theme.Shared.Components;
using Aiwins.Rocket.DependencyInjection;

namespace MyCompanyName.MyProjectName.Web
{
    [Dependency(ReplaceServices = true)]
    public class MyProjectNameBrandingProvider : DefaultBrandingProvider
    {
        public override string AppName => "MyProjectName";
    }
}
