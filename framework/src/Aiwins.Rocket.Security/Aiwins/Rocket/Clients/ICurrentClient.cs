namespace Aiwins.Rocket.Clients {
    public interface ICurrentClient {
        string Id { get; }

        bool IsAuthenticated { get; }
    }
}