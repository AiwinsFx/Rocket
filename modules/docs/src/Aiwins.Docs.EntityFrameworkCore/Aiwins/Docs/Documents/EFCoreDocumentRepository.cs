using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Aiwins.Rocket.Domain.Repositories.EntityFrameworkCore;
using Aiwins.Rocket.EntityFrameworkCore;
using Aiwins.Docs.EntityFrameworkCore;

namespace Aiwins.Docs.Documents
{
    public class EFCoreDocumentRepository : EfCoreRepository<IDocsDbContext, Document>, IDocumentRepository
    {
        public EFCoreDocumentRepository(IDbContextProvider<IDocsDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }

        public async Task<List<Document>> GetListByProjectId(Guid projectId,
            CancellationToken cancellationToken = default)
        {
            return await DbSet.Where(d => d.ProjectId == projectId).ToListAsync(cancellationToken: cancellationToken);
        }

        public async Task<Document> FindAsync(Guid projectId, string name, string languageCode, string version,
            bool includeDetails = true,
            CancellationToken cancellationToken = default)
        {
            return await DbSet.IncludeDetails(includeDetails)
                .FirstOrDefaultAsync(x =>
                    x.ProjectId == projectId && x.Name == name && x.LanguageCode == languageCode &&
                    x.Version == version,
                cancellationToken);
        }

        public async Task DeleteAsync(Guid projectId, string name, string languageCode, string version, CancellationToken cancellationToken = default)
        {
            await DeleteAsync(x =>
                x.ProjectId == projectId && x.Name == name && x.LanguageCode == languageCode &&
                x.Version == version, cancellationToken: cancellationToken);
        }
    }
}