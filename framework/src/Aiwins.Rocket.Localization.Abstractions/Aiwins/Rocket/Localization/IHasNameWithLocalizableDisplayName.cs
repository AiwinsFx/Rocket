using JetBrains.Annotations;

namespace Aiwins.Rocket.Localization {
    public interface IHasNameWithLocalizableDisplayName {
        [NotNull]
        public string Name { get; }

        [CanBeNull]
        public ILocalizableString DisplayName { get; }
    }
}