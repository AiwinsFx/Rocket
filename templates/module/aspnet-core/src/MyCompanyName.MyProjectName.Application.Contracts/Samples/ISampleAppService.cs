using System.Threading.Tasks;
using Aiwins.Rocket.Application.Services;

namespace MyCompanyName.MyProjectName.Samples
{
    public interface ISampleAppService : IApplicationService
    {
        Task<SampleDto> GetAsync();

        Task<SampleDto> GetAuthorizedAsync();
    }
}
