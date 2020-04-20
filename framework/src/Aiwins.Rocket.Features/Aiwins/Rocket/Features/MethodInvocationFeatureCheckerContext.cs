using System.Reflection;

namespace Aiwins.Rocket.Features
{
    public class MethodInvocationFeatureCheckerContext
    {
        public MethodInfo Method { get; }

        public MethodInvocationFeatureCheckerContext(MethodInfo method)
        {
            Method = method;
        }
    }
}