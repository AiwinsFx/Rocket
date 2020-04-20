namespace Aiwins.Rocket.Data {
    public class RocketDataSeedOptions {
        public DataSeedContributorList Contributors { get; }

        public RocketDataSeedOptions () {
            Contributors = new DataSeedContributorList ();
        }
    }
}