using Microsoft.EntityFrameworkCore;
using Aiwins.Rocket.Data;
using Aiwins.Rocket.EntityFrameworkCore;
using Aiwins.Docs.Documents;
using Aiwins.Docs.Projects;

namespace Aiwins.Docs.EntityFrameworkCore
{
    [ConnectionStringName(DocsDbProperties.ConnectionStringName)]
    public class DocsDbContext: RocketDbContext<DocsDbContext>, IDocsDbContext
    {
        public DbSet<Project> Projects { get; set; }

        public DbSet<Document> Documents { get; set; }

        public DbSet<DocumentContributor> DocumentContributors { get; set; }

        public DocsDbContext(DbContextOptions<DocsDbContext> options) 
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ConfigureDocs();
        }
    }
}
