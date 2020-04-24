using System.Threading.Tasks;

namespace Aiwins.Rocket.SettingManagement.Web.Pages.SettingManagement
{
    public interface ISettingPageContributor
    {
        Task ConfigureAsync(SettingPageCreationContext context);

        Task<bool> CheckPermissionsAsync(SettingPageCreationContext context);
    }
}