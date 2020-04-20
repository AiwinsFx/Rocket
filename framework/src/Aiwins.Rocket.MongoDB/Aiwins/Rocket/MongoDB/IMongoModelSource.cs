namespace Aiwins.Rocket.MongoDB {
    public interface IMongoModelSource {
        MongoDbContextModel GetModel (RocketMongoDbContext dbContext);
    }
}