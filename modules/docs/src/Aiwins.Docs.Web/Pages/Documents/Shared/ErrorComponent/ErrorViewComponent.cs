using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Aiwins.Rocket.AspNetCore.Mvc;

namespace Aiwins.Docs.Pages.Documents.Shared.ErrorComponent
{
    public class ErrorViewComponent : RocketViewComponent
    {
        public IViewComponentResult Invoke(ErrorPageModel model)
        {
            return View("~/Pages/Documents/Shared/ErrorComponent/Default.cshtml", model);
        }
    }
}
