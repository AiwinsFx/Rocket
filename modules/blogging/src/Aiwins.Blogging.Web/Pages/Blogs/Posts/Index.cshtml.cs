using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Aiwins.Rocket.AspNetCore.Mvc.UI.RazorPages;
using Aiwins.Blogging.Blogs;
using Aiwins.Blogging.Blogs.Dtos;
using Aiwins.Blogging.Posts;
using Aiwins.Blogging.Tagging;
using Aiwins.Blogging.Tagging.Dtos;

namespace Aiwins.Blogging.Pages.Blog.Posts
{
    public class IndexModel : BloggingPageModel
    {
        private readonly IPostAppService _postAppService;
        private readonly IBlogAppService _blogAppService;
        private readonly ITagAppService _tagAppService;

        [BindProperty(SupportsGet = true)]
        public string BlogShortName { get; set; }

        [BindProperty(SupportsGet = true)]
        public string TagName { get; set; }

        public BlogDto Blog { get; set; }

        public IReadOnlyList<PostWithDetailsDto> Posts { get; set; }

        public IReadOnlyList<TagDto> PopularTags { get; set; }

        public IndexModel(IPostAppService postAppService, IBlogAppService blogAppService, ITagAppService tagAppService)
        {
            _postAppService = postAppService;
            _blogAppService = blogAppService;
            _tagAppService = tagAppService;
        }

        public virtual async Task<ActionResult> OnGetAsync()
        {
            Blog = await _blogAppService.GetByShortNameAsync(BlogShortName);
            Posts = (await _postAppService.GetListByBlogIdAndTagName(Blog.Id, TagName)).Items;
            PopularTags = (await _tagAppService.GetPopularTags(Blog.Id, new GetPopularTagsInput {ResultCount = 10, MinimumPostCount = 2}));

            return Page();
        }
    }
}