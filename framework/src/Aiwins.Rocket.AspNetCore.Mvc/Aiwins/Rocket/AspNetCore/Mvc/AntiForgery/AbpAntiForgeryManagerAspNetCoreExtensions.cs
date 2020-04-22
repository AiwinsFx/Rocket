namespace Aiwins.Rocket.AspNetCore.Mvc.AntiForgery {
    public static class RocketAntiForgeryManagerAspNetCoreExtensions {
        public static void SetCookie (this IRocketAntiForgeryManager manager) {
            manager.HttpContext.Response.Cookies.Append (manager.Options.TokenCookieName, manager.GenerateToken ());
        }
    }
}