using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.ApiExplorer;

namespace Aiwins.Rocket.AspNetCore.Mvc.ApiExploring {
    public class RocketRemoteServiceApiDescriptionProviderOptions {
        public HashSet<ApiResponseType> SupportedResponseTypes { get; set; } = new HashSet<ApiResponseType> ();
    }
}