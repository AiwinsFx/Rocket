using System.Collections.Generic;
using Aiwins.Rocket.Http.Modeling;
using Aiwins.Rocket.Reflection;

namespace Aiwins.Rocket.Http.Client.DynamicProxying {
    internal static class HttpActionParameterHelper {
        public static object FindParameterValue (IReadOnlyDictionary<string, object> methodArguments, ParameterApiDescriptionModel apiParameter) {
            var value = methodArguments.GetOrDefault (apiParameter.NameOnMethod);
            if (value == null) {
                return null;
            }

            if (apiParameter.Name == apiParameter.NameOnMethod) {
                return value;
            }

            return ReflectionHelper.GetValueByPath (value, value.GetType (), apiParameter.Name);
        }
    }
}