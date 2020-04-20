using Microsoft.Extensions.Localization;

namespace Aiwins.Rocket.Localization {
    public interface ITemplateLocalizer {
        string Localize (IStringLocalizer localizer, string text);
    }
}