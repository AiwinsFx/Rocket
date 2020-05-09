using MongoDB.Driver;
using Aiwins.Rocket.Data;
using Aiwins.Rocket.MongoDB;
using Aiwins.Docs.Documents;
using Aiwins.Docs.Projects;

namespace Aiwins.Docs.MongoDB
{
    [ConnectionStringName(DocsDbProperties.ConnectionStringName)]
    public interface IDocsMongoDbContext : IRocketMongoDbContext
    {
        IMongoCollection<Project> Projects { get; }

        IMongoCollection<Document> Documents { get; }
    }
}