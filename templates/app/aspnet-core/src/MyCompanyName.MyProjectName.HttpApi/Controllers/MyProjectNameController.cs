using MyCompanyName.MyProjectName.Localization;
using Aiwins.Rocket.AspNetCore.Mvc;

namespace MyCompanyName.MyProjectName.Controllers
{
    /* Inherit your controllers from this class.
     */
    public abstract class MyProjectNameController : RocketController
    {
        protected MyProjectNameController()
        {
            LocalizationResource = typeof(MyProjectNameResource);
        }
    }
}