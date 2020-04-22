using Microsoft.AspNetCore.Http;

namespace Aiwins.Rocket.AspNetCore.Mvc.AntiForgery {
    public interface IRocketAntiForgeryManager {
        RocketAntiForgeryOptions Options { get; }

        HttpContext HttpContext { get; }

        void SetCookie ();

        string GenerateToken ();
    }
}