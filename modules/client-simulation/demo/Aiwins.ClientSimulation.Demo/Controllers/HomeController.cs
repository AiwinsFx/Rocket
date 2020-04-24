using Microsoft.AspNetCore.Mvc;
using Aiwins.Rocket.AspNetCore.Mvc;

namespace Aiwins.ClientSimulation.Demo.Controllers
{
    public class HomeController : RocketController
    {
        public ActionResult Index()
        {
            return Redirect("/ClientSimulation");
        }
    }
}
