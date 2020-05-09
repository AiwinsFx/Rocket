using Aiwins.Rocket.AspNetCore.Mvc;
using Aiwins.Blogging.Localization;

namespace Aiwins.Blogging.Areas.Blog.Controllers
{
    public abstract class BloggingControllerBase : RocketController
    {
        protected BloggingControllerBase()
        {
            ObjectMapperContext = typeof(BloggingWebModule);
            LocalizationResource = typeof(BloggingResource);
        }
    }
}