using System.Threading.Tasks;
using IdentityServer4.Stores;
using Aiwins.Rocket.ObjectMapping;

namespace Aiwins.Rocket.IdentityServer.Clients
{
    public class ClientStore : IClientStore
    {
        protected IClientRepository ClientRepository { get; }
        protected IObjectMapper<RocketIdentityServerDomainModule> ObjectMapper { get; }

        public ClientStore(IClientRepository clientRepository, IObjectMapper<RocketIdentityServerDomainModule> objectMapper)
        {
            ClientRepository = clientRepository;
            ObjectMapper = objectMapper;
        }

        public virtual async Task<IdentityServer4.Models.Client> FindClientByIdAsync(string clientId)
        {
            var client = await ClientRepository.FindByClientIdAsync(clientId);
            return ObjectMapper.Map<Client, IdentityServer4.Models.Client>(client);
        }
    }
}
