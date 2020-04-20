namespace Aiwins.Rocket.IdentityModel {
    public class RocketIdentityClientOptions {
        public IdentityClientConfigurationDictionary IdentityClients { get; set; }

        public RocketIdentityClientOptions () {
            IdentityClients = new IdentityClientConfigurationDictionary ();
        }
    }
}