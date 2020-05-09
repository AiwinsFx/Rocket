using Aiwins.Rocket.AspNetCore.Mvc.UI.RazorPages;

namespace Aiwins.Docs.Admin.Pages.Docs.Admin
{
    public abstract class DocsAdminPageModel : RocketPageModel
    {
        public DocsAdminPageModel()
        {
            ObjectMapperContext = typeof(DocsAdminWebModule);
        }
    }
}