using Aiwins.Rocket.AspNetCore.Mvc.UI.RazorPages;

namespace Aiwins.Blogging.Pages.Blog
{
    public abstract class BloggingPageModel : RocketPageModel
    {
        public BloggingPageModel()
        {
            ObjectMapperContext = typeof(BloggingWebModule);
        }
    }
}