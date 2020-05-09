using JetBrains.Annotations;

namespace Microsoft.Extensions.Localization {
    public interface IRocketStringLocalizerFactoryWithDefaultResourceSupport {
        [CanBeNull]
        IStringLocalizer CreateDefaultOrNull ();
    }
}