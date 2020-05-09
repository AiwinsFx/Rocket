using MongoDB.Driver;
using Aiwins.Rocket.Data;
using Aiwins.Docs.Projects;
using Aiwins.Rocket.MongoDB;
using Aiwins.Docs.Documents;

namespace Aiwins.Docs.MongoDB
{
    [ConnectionStringName(DocsDbProperties.ConnectionStringName)]
    public class DocsMongoDbContext : RocketMongoDbContext, IDocsMongoDbContext
    {
        public IMongoCollection<Project> Projects => Collection<Project>();
        public IMongoCollection<Document> Documents => Collection<Document>();

        protected override void CreateModel(IMongoModelBuilder modelBuilder)
        {
            base.CreateModel(modelBuilder);

            modelBuilder.ConfigureDocs();
        }
    }
}
