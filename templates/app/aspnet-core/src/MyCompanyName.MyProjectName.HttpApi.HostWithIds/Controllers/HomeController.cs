using Microsoft.AspNetCore.Mvc;
using Aiwins.Rocket.AspNetCore.Mvc;

namespace MyCompanyName.MyProjectName.Controllers
{
    public class HomeController : RocketController
    {
        public ActionResult Index()
        {
            return Redirect("/swagger");
        }
    }
}
