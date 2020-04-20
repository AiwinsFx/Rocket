namespace Aiwins.Rocket.Http.Client {
    public class RocketRemoteServiceOptions {
        public RemoteServiceConfigurationDictionary RemoteServices { get; set; }

        public RocketRemoteServiceOptions () {
            RemoteServices = new RemoteServiceConfigurationDictionary ();
        }
    }
}