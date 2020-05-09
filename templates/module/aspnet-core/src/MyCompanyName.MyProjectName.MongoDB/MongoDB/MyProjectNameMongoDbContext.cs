using Aiwins.Rocket.Data;
using Aiwins.Rocket.MongoDB;

namespace MyCompanyName.MyProjectName.MongoDB
{
    [ConnectionStringName(MyProjectNameDbProperties.ConnectionStringName)]
    public class MyProjectNameMongoDbContext : RocketMongoDbContext, IMyProjectNameMongoDbContext
    {
        /* Add mongo collections here. Example:
         * public IMongoCollection<Question> Questions => Collection<Question>();
         */

        protected override void CreateModel(IMongoModelBuilder modelBuilder)
        {
            base.CreateModel(modelBuilder);

            modelBuilder.ConfigureMyProjectName();
        }
    }
}