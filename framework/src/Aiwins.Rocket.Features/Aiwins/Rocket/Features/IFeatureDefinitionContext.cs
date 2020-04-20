using JetBrains.Annotations;
using Aiwins.Rocket.Localization;

namespace Aiwins.Rocket.Features
{
    public interface IFeatureDefinitionContext
    {
        FeatureGroupDefinition AddGroup([NotNull] string name, ILocalizableString displayName = null);

        FeatureGroupDefinition GetGroupOrNull(string name);
        
        void RemoveGroup(string name);
    }
}