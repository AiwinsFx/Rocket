namespace Microsoft.Extensions.Localization {
    public static class RocketStringLocalizerFactoryExtensions {
        public static IStringLocalizer CreateDefaultOrNull (this IStringLocalizerFactory localizerFactory) {
            return (localizerFactory as IRocketStringLocalizerFactoryWithDefaultResourceSupport) ?
                .CreateDefaultOrNull ();
        }
    }
}