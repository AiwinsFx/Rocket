using System.Threading.Tasks;

namespace Aiwins.Rocket.IdentityServer.IdentityResources
{
    public interface IIdentityResourceDataSeeder
    {
        Task CreateStandardResourcesAsync();
    }
}