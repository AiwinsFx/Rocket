using System.Threading.Tasks;

namespace Aiwins.Rocket.UI.Navigation
{
    public interface IMenuManager
    {
        Task<ApplicationMenu> GetAsync(string name);
    }
}
