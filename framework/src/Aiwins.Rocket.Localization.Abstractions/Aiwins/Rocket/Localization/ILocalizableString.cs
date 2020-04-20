using Microsoft.Extensions.Localization;

namespace Aiwins.Rocket.Localization {
    public interface ILocalizableString {
        LocalizedString Localize (IStringLocalizerFactory stringLocalizerFactory);
    }
}