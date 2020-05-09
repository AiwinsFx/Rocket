using Microsoft.EntityFrameworkCore;
using Aiwins.Rocket.Data;
using Aiwins.Rocket.EntityFrameworkCore;
using Aiwins.Docs.Documents;
using Aiwins.Docs.Projects;

namespace Aiwins.Docs.EntityFrameworkCore
{
    [ConnectionStringName(DocsDbProperties.ConnectionStringName)]
    public interface IDocsDbContext : IEfCoreDbContext
    {
        DbSet<Project> Projects { get; set; }

        DbSet<Document> Documents { get; set; }

        DbSet<DocumentContributor> DocumentContributors { get; set; }
    }
}