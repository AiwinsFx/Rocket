using Microsoft.AspNetCore.Mvc.Razor.Internal;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Aiwins.Rocket.Users;

namespace Aiwins.Rocket.AspNetCore.Mvc.UI.RazorPages
{
    public abstract class RocketPage : Page
    {
        [RazorInject]
        public ICurrentUser CurrentUser { get; set; }
    }
}
