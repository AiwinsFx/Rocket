using Aiwins.Rocket.Data;
using Aiwins.Rocket.MongoDB;

namespace MyCompanyName.MyProjectName.MongoDB
{
    [ConnectionStringName(MyProjectNameDbProperties.ConnectionStringName)]
    public interface IMyProjectNameMongoDbContext : IRocketMongoDbContext
    {
        /* Define mongo collections here. Example:
         * IMongoCollection<Question> Questions { get; }
         */
    }
}
