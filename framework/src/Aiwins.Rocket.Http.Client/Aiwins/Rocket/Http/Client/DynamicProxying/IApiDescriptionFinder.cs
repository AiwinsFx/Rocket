using System;
using System.Reflection;
using System.Threading.Tasks;
using Aiwins.Rocket.Http.Modeling;

namespace Aiwins.Rocket.Http.Client.DynamicProxying {
    public interface IApiDescriptionFinder {
        Task<ActionApiDescriptionModel> FindActionAsync (string baseUrl, Type serviceType, MethodInfo invocationMethod);

        Task<ApplicationApiDescriptionModel> GetApiDescriptionAsync (string baseUrl);
    }
}