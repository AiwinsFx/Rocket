using Aiwins.Rocket.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc;

namespace Aiwins.Rocket.Account.Web.Modules.Account.Components.Toolbar.UserLoginLink {
    public class UserLoginLinkViewComponent : RocketViewComponent {
        public virtual IViewComponentResult Invoke () {
            return View ("~/Modules/Account/Components/Toolbar/UserLoginLink/Default.cshtml");
        }
    }
}