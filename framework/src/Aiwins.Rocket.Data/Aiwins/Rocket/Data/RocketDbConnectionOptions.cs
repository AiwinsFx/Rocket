namespace Aiwins.Rocket.Data {
    public class RocketDbConnectionOptions {
        public ConnectionStrings ConnectionStrings { get; set; }

        public RocketDbConnectionOptions () {
            ConnectionStrings = new ConnectionStrings ();
        }
    }
}