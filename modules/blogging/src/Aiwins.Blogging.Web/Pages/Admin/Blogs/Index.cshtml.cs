using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Aiwins.Rocket.AspNetCore.Mvc.UI.RazorPages;
using Aiwins.Blogging.Pages.Blog;

namespace Aiwins.Blogging.Pages.Admin.Blogs
{
    public class IndexModel : BloggingPageModel
    {
        private readonly IAuthorizationService _authorization;

        public IndexModel(IAuthorizationService authorization)
        {
            _authorization = authorization;
        }

        public virtual async Task<ActionResult> OnGetAsync()
        {
            if (!await _authorization.IsGrantedAsync(BloggingPermissions.Blogs.Management))
            {
                return Redirect("/");
            }

            return Page();
        }
    }
}