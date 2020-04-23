using Aiwins.Rocket.AspNetCore.Mvc.UI.Theming;
using Aiwins.Rocket.DependencyInjection;

namespace Aiwins.Rocket.AspNetCore.Mvc.UI.Theme.Basic
{
    [ThemeName(Name)]
    public class BasicTheme : ITheme, ITransientDependency
    {
        public const string Name = "Basic";

        public virtual string GetLayout(string name, bool fallbackToDefault = true)
        {
            switch (name)
            {
                case StandardLayouts.Application:
                    return "~/Themes/Basic/Layouts/Application.cshtml";
                case StandardLayouts.Account:
                    return "~/Themes/Basic/Layouts/Account.cshtml";
                case StandardLayouts.Empty:
                    return "~/Themes/Basic/Layouts/Empty.cshtml";
                default:
                    return fallbackToDefault ? "~/Themes/Basic/Layouts/Application.cshtml" : null;
            }
        }
    }
}
