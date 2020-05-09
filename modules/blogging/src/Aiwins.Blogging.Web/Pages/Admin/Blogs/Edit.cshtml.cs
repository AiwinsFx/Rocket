using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Aiwins.Rocket.AspNetCore.Mvc.UI.RazorPages;
using Aiwins.Blogging.Blogs;
using Aiwins.Blogging.Blogs.Dtos;
using Aiwins.Blogging.Pages.Blog;
using Aiwins.Blogging.Posts;

namespace Aiwins.Blogging.Pages.Admin.Blogs
{
    public class EditModel : BloggingPageModel
    {
        private readonly IBlogAppService _blogAppService;
        private readonly IAuthorizationService _authorization;

        [BindProperty(SupportsGet = true)]
        public Guid BlogId { get; set; }

        [BindProperty]
        public BlogEditViewModel Blog { get; set; } = new BlogEditViewModel();

        public EditModel(IBlogAppService blogAppService, IAuthorizationService authorization)
        {
            _blogAppService = blogAppService;
            _authorization = authorization;
        }

        public virtual async Task<ActionResult> OnGetAsync()
        {
            if (!await _authorization.IsGrantedAsync(BloggingPermissions.Blogs.Update))
            {
                return Redirect("/");
            }

            var blog = await _blogAppService.GetAsync(BlogId);

            Blog = ObjectMapper.Map<BlogDto, BlogEditViewModel>(blog);

            return Page();
        }

        public virtual async Task<IActionResult> OnPostAsync()
        {
            await _blogAppService.Update(Blog.Id, new UpdateBlogDto()
            {
                Name = Blog.Name,
                ShortName = Blog.ShortName,
                Description = Blog.Description
            });

            return Page();
        }

        public class BlogEditViewModel
        {
            [HiddenInput]
            [Required]
            public Guid Id { get; set; }

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