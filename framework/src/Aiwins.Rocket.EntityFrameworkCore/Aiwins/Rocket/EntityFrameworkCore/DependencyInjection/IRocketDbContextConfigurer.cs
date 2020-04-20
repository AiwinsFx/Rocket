namespace Aiwins.Rocket.EntityFrameworkCore.DependencyInjection {
    public interface IRocketDbContextConfigurer {
        void Configure (RocketDbContextConfigurationContext context);
    }

    public interface IRocketDbContextConfigurer<TDbContext>
        where TDbContext : RocketDbContext<TDbContext> {
            void Configure (RocketDbContextConfigurationContext<TDbContext> context);
        }
}