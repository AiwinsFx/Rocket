namespace Aiwins.Rocket.EntityFrameworkCore {
    public interface IRocketEfCoreDbContext : IEfCoreDbContext {
        void Initialize (RocketEfCoreDbContextInitializationContext initializationContext);
    }
}