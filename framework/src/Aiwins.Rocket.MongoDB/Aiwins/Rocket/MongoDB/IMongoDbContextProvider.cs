namespace Aiwins.Rocket.MongoDB {
    public interface IMongoDbContextProvider<out TMongoDbContext>
        where TMongoDbContext : IRocketMongoDbContext {
            TMongoDbContext GetDbContext ();
        }
}