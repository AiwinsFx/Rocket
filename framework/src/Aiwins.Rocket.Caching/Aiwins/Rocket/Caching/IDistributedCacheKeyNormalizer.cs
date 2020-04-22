namespace Aiwins.Rocket.Caching {
    public interface IDistributedCacheKeyNormalizer {
        string NormalizeKey (DistributedCacheKeyNormalizeArgs args);
    }
}