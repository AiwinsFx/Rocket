using System.Threading.Tasks;
using Aiwins.Rocket.AspNetCore.Mvc.ApplicationConfigurations;

namespace Aiwins.Rocket.AspNetCore.Mvc.Client {
    public interface ICachedApplicationConfigurationClient {
        Task<ApplicationConfigurationDto> GetAsync ();
    }
}