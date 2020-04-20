using Aiwins.Rocket.DependencyInjection;

namespace Aiwins.Rocket.Settings {
    public abstract class SettingDefinitionProvider : ISettingDefinitionProvider, ITransientDependency {
        public abstract void Define (ISettingDefinitionContext context);
    }
}