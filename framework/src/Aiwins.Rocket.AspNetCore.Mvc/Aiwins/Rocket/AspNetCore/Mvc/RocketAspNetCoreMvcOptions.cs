using Aiwins.Rocket.AspNetCore.Mvc.Conventions;

namespace Aiwins.Rocket.AspNetCore.Mvc {
    public class RocketAspNetCoreMvcOptions {
        public bool? MinifyGeneratedScript { get; set; }

        public RocketConventionalControllerOptions ConventionalControllers { get; }

        public RocketAspNetCoreMvcOptions () {
            ConventionalControllers = new RocketConventionalControllerOptions ();
        }
    }
}