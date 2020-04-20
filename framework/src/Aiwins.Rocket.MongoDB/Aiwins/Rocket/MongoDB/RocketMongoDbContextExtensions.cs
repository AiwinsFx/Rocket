namespace Aiwins.Rocket.MongoDB {
    public static class RocketMongoDbContextExtensions {
        public static RocketMongoDbContext ToRocketMongoDbContext (this IRocketMongoDbContext dbContext) {
            var rocketMongoDbContext = dbContext as RocketMongoDbContext;

            if (rocketMongoDbContext == null) {
                throw new RocketException ($"The type {dbContext.GetType().AssemblyQualifiedName} should be convertable to {typeof(RocketMongoDbContext).AssemblyQualifiedName}!");
            }

            return rocketMongoDbContext;
        }
    }
}