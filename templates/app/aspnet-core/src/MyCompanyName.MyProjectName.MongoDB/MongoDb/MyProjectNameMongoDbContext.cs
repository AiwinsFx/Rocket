using MongoDB.Driver;
using MyCompanyName.MyProjectName.Users;
using Aiwins.Rocket.Data;
using Aiwins.Rocket.MongoDB;

namespace MyCompanyName.MyProjectName.MongoDB
{
    [ConnectionStringName("Default")]
    public class MyProjectNameMongoDbContext : RocketMongoDbContext
    {
        public IMongoCollection<User> Users => Collection<User>();

        protected override void CreateModel(IMongoModelBuilder modelBuilder)
        {
            base.CreateModel(modelBuilder);

            modelBuilder.Entity<User>(b =>
            {
                /* Sharing the same "RocketUsers" collection
                 * with the Identity module's IdentityUser class. */
                b.CollectionName = "RocketUsers";
            });
        }
    }
}
