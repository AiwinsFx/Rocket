using Aiwins.Rocket.Localization;
using JetBrains.Annotations;

namespace Aiwins.Rocket.Features {
    public interface IFeatureDefinitionContext {
        FeatureGroupDefinition AddGroup ([NotNull] string name, ILocalizableString displayName = null);

        FeatureGroupDefinition GetGroupOrNull (string name);

        void RemoveGroup (string name);
    }
}