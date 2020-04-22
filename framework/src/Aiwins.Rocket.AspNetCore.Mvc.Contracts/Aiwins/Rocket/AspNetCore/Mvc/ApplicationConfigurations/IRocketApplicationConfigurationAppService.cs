using System.Threading.Tasks;
using Aiwins.Rocket.Application.Services;

namespace Aiwins.Rocket.AspNetCore.Mvc.ApplicationConfigurations {
    public interface IRocketApplicationConfigurationAppService : IApplicationService {
        Task<ApplicationConfigurationDto> GetAsync ();
    }
}