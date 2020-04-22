using Microsoft.AspNetCore.Cors.Infrastructure;

namespace Microsoft.AspNetCore.Cors {
    public static class RocketCorsPolicyBuilderExtensions {
        public static CorsPolicyBuilder WithRocketExposedHeaders (this CorsPolicyBuilder corsPolicyBuilder) {
            return corsPolicyBuilder.WithExposedHeaders ("_RocketErrorFormat");
        }
    }
}