using System;
using System.Threading.Tasks;
using Aiwins.Rocket.Http.Modeling;

namespace Aiwins.Rocket.Http.Client.DynamicProxying {
    public interface IApiDescriptionCache {
        Task<ApplicationApiDescriptionModel> GetAsync (
            string baseUrl,
            Func<Task<ApplicationApiDescriptionModel>> factory
        );
    }
}