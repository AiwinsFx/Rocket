using System.Threading.Tasks;

namespace Aiwins.Rocket.AspNetCore.Mvc.UI.Theme.Shared.Toolbars
{
    public interface IToolbarManager
    {
        Task<Toolbar> GetAsync(string name);
    }
}
