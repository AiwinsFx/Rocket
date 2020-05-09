using Aiwins.Rocket.Application.Services;
using Aiwins.Blogging.Localization;

namespace Aiwins.Blogging
{
    public abstract class BloggingAppServiceBase : ApplicationService
    {
        protected BloggingAppServiceBase()
        {
            ObjectMapperContext = typeof(BloggingApplicationModule);
            LocalizationResource = typeof(BloggingResource);
        }
    }
}