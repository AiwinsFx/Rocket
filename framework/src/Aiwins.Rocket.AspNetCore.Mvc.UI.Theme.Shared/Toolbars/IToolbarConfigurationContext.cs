using Aiwins.Rocket.AspNetCore.Mvc.UI.Theming;
using Aiwins.Rocket.DependencyInjection;

namespace Aiwins.Rocket.AspNetCore.Mvc.UI.Theme.Shared.Toolbars
{
    public interface IToolbarConfigurationContext : IServiceProviderAccessor
    {
        ITheme Theme { get; }

        Toolbar Toolbar { get; }
    }
}