using MyCompanyName.MyProjectName.Localization;
using Aiwins.Rocket.Application.Services;

namespace MyCompanyName.MyProjectName
{
    public abstract class MyProjectNameAppService : ApplicationService
    {
        protected MyProjectNameAppService()
        {
            LocalizationResource = typeof(MyProjectNameResource);
            ObjectMapperContext = typeof(MyProjectNameApplicationModule);
        }
    }
}
