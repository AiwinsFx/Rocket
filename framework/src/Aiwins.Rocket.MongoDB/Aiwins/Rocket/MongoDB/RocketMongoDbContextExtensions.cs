namespace Aiwins.Rocket.MongoDB {
    public static class RocketMongoDbContextExtensions {
        public static RocketMongoDbContext ToRocketMongoDbContext (this IRocketMongoDbContext dbContext) {
            var abpMongoDbContext = dbContext as RocketMongoDbContext;

            if (abpMongoDbContext == null) {
                throw new RocketException ($"The type {dbContext.GetType().AssemblyQualifiedName} should be convertable to {typeof(RocketMongoDbContext).AssemblyQualifiedName}!");
            }

            return abpMongoDbContext;
        }
    }
}