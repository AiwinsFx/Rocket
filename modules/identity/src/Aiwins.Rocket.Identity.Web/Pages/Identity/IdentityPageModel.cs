using Aiwins.Rocket.AspNetCore.Mvc.UI.RazorPages;

namespace Aiwins.Rocket.Identity.Web.Pages.Identity {
    public abstract class IdentityPageModel : RocketPageModel {
        protected IdentityPageModel () {
            ObjectMapperContext = typeof (RocketIdentityWebModule);
        }
    }
}