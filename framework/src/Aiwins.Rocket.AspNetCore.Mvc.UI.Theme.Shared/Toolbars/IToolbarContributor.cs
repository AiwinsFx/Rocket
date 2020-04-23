using System.Threading.Tasks;

namespace Aiwins.Rocket.AspNetCore.Mvc.UI.Theme.Shared.Toolbars
{
    public interface IToolbarContributor
    {
        Task ConfigureToolbarAsync(IToolbarConfigurationContext context);
    }
}