namespace Aiwins.Rocket.AspNetCore.SignalR {
    public class RocketSignalROptions {
        public HubConfigList Hubs { get; }

        public RocketSignalROptions () {
            Hubs = new HubConfigList ();
        }
    }
}