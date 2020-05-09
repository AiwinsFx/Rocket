using MyCompanyName.MyProjectName.Localization;
using Aiwins.Rocket.AspNetCore.Mvc;

namespace MyCompanyName.MyProjectName
{
    public abstract class MyProjectNameController : RocketController
    {
        protected MyProjectNameController()
        {
            LocalizationResource = typeof(MyProjectNameResource);
        }
    }
}
