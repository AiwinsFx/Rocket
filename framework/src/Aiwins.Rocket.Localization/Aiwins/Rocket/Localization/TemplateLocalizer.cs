using System.Text.RegularExpressions;
using Aiwins.Rocket.DependencyInjection;
using Microsoft.Extensions.Localization;

namespace Aiwins.Rocket.Localization {
    public class TemplateLocalizer : ITemplateLocalizer, ITransientDependency {
        public string Localize (IStringLocalizer localizer, string text) {
            return new Regex ("\\{\\{#L:.+?\\}\\}")
                .Replace (
                    text,
                    match => localizer[match.Value.Substring (5, match.Length - 7)]
                );
        }
    }
}