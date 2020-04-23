using System.Linq;
using Microsoft.Extensions.Options;
using Aiwins.Rocket.DependencyInjection;

namespace Aiwins.Rocket.AspNetCore.Mvc.UI.Theming
{
    public class DefaultThemeSelector : IThemeSelector, ITransientDependency
    {
        protected RocketThemingOptions Options { get; }

        public DefaultThemeSelector(IOptions<RocketThemingOptions> options)
        {
            Options = options.Value;
        }

        public virtual ThemeInfo GetCurrentThemeInfo()
        {
            if (!Options.Themes.Any())
            {
                throw new RocketException($"No theme registered! Use {nameof(RocketThemingOptions)} to register themes.");
            }

            if (Options.DefaultThemeName == null)
            {
                return Options.Themes.Values.First();
            }

            var themeInfo = Options.Themes.Values.FirstOrDefault(t => t.Name == Options.DefaultThemeName);
            if (themeInfo == null)
            {
                throw new RocketException("Default theme is configured but it's not found in the registered themes: " + Options.DefaultThemeName);
            }

            return themeInfo;
        }
    }
}