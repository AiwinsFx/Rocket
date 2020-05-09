using Aiwins.Rocket.Application.Services;
using Aiwins.Docs.Localization;

namespace Aiwins.Docs
{
    public abstract class DocsAppServiceBase : ApplicationService
    {
        protected DocsAppServiceBase()
        {
            ObjectMapperContext = typeof(DocsApplicationModule);
            LocalizationResource = typeof(DocsResource);
        }
    }
}