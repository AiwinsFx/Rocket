using System.Threading.Tasks;

namespace Aiwins.Rocket.UI.Navigation
{
    public interface IMenuContributor
    {
        Task ConfigureMenuAsync(MenuConfigurationContext context);
    }
}