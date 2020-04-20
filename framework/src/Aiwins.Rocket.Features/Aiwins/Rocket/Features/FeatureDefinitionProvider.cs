using Aiwins.Rocket.DependencyInjection;

namespace Aiwins.Rocket.Features {
    public abstract class FeatureDefinitionProvider : IFeatureDefinitionProvider, ITransientDependency {
        public abstract void Define (IFeatureDefinitionContext context);
    }
}