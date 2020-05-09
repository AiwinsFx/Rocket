using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Aiwins.Rocket.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;
using Aiwins.Rocket.AspNetCore.Mvc.UI.RazorPages;
using Aiwins.Blogging.Blogs;
using Aiwins.Blogging.Blogs.Dtos;
using Aiwins.Blogging.Pages.Blog;

namespace Aiwins.Blogging.Pages.Admin.Blogs
{
    public class CreateModel : BloggingPageModel
    {
        private readonly IBlogAppService _blogAppService;
        private readonly IAuthorizationService _authorization;

        [BindProperty]
        public BlogCreateModalView Blog { get; set; } = new BlogCreateModalView();

        public CreateModel(IBlogAppService blogAppService, IAuthorizationService authorization)
        {
            _blogAppService = blogAppService;
            _authorization = authorization;
        }

        public virtual async Task<ActionResult> OnGetAsync()
        {
            if (!await _authorization.IsGrantedAsync(BloggingPermissions.Blogs.Create))
            {
                return Redirect("/");
            }

            return Page();
        }

        public virtual async Task<IActionResult> OnPostAsync()
        {
            var blogDto = ObjectMapper.Map<BlogCreateModalView, CreateBlogDto>(Blog);

            await _blogAppService.Create(blogDto);

            return NoContent();
        }


        public class BlogCreateModalView
        {
            [Required]
            [StringLength(BlogConsts.MaxNameLength)]
            public string Name { get; set; }

            [Required]
            [StringLength(BlogConsts.MaxShortNameLength)]
            public string ShortName { get; set; }

            [StringLength(BlogConsts.MaxDescriptionLength)]
            public string Description { get; set; }

        }
    }
}