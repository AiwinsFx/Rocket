using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Aiwins.Rocket.AspNetCore.Mvc.UI.RazorPages;
using Aiwins.Blogging.Blogs;
using Aiwins.Blogging.Blogs.Dtos;

namespace Aiwins.Blogging.Pages.Blog
{
    public class IndexModel : RocketPageModel
    {
        private readonly IBlogAppService _blogAppService;

        public IReadOnlyList<BlogDto> Blogs { get; private set; }

        public IndexModel(IBlogAppService blogAppService)
        {
            _blogAppService = blogAppService;
        }

        public virtual async Task<IActionResult> OnGetAsync()
        {
            var result = await _blogAppService.GetListAsync();

            if (result.Items.Count == 1)
            {
                var blog = result.Items[0];
                return RedirectToPage("./Posts/Index", new { blogShortName = blog.ShortName });
            }

            Blogs = result.Items;

            return Page();
        }
    }
}