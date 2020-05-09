using System.Net.Http;
using System.Threading.Tasks;

namespace Aiwins.Rocket.Cli.ProjectBuilding
{
    public interface IRemoteServiceExceptionHandler
    {
        Task EnsureSuccessfulHttpResponseAsync(HttpResponseMessage responseMessage);

        Task<string> GetRocketRemoteServiceErrorAsync(HttpResponseMessage responseMessage);
    }
}