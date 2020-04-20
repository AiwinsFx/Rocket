using Aiwins.Rocket.Localization;

namespace Aiwins.Rocket.ExceptionHandling {
    public interface ILocalizeErrorMessage {
        string LocalizeMessage (LocalizationContext context);
    }
}