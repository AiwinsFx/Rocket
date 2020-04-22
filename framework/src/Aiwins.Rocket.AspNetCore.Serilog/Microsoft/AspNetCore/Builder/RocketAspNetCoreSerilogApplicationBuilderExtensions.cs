using Aiwins.Rocket.AspNetCore.Serilog;

namespace Microsoft.AspNetCore.Builder {
    public static class RocketAspNetCoreSerilogApplicationBuilderExtensions {
        public static IApplicationBuilder UseRocketSerilogEnrichers (this IApplicationBuilder app) {
            return app
                .UseMiddleware<RocketSerilogMiddleware> ();
        }
    }
}