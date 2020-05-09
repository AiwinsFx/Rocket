using MyCompanyName.MyProjectName.Localization;
using Aiwins.Rocket.AspNetCore.Mvc.UI.RazorPages;

namespace MyCompanyName.MyProjectName.Web.Pages
{
    /* Inherit your PageModel classes from this class.
     */
    public abstract class MyProjectNamePageModel : RocketPageModel
    {
        protected MyProjectNamePageModel()
        {
            LocalizationResourceType = typeof(MyProjectNameResource);
            ObjectMapperContext = typeof(MyProjectNameWebModule);
        }
    }
}