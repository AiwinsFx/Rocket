using Aiwins.Rocket.Http;

namespace Aiwins.Rocket.AspNetCore.Mvc.UI.Theme.Shared.Views.Error
{
    public class RocketErrorViewModel
    {
        public RemoteServiceErrorInfo ErrorInfo { get; set; }

        public int HttpStatusCode { get; set; }
    }
}
