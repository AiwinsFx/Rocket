using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Aiwins.Rocket.AspNetCore.Mvc.UI.RazorPages;

namespace Aiwins.Docs.Admin.Pages.Docs.Admin.Projects
{
    [Authorize(DocsAdminPermissions.Projects.Default)]
    public class IndexModel : DocsAdminPageModel
    {
        public virtual Task<IActionResult> OnGet()
        {
            return Task.FromResult<IActionResult>(Page());
        }
    }
}