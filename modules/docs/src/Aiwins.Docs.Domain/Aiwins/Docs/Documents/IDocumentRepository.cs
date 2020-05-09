using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Aiwins.Rocket.Domain.Repositories;

namespace Aiwins.Docs.Documents
{
    public interface IDocumentRepository : IBasicRepository<Document>
    {
        Task<List<Document>> GetListByProjectId(Guid projectId,
            CancellationToken cancellationToken = default);

        Task<Document> FindAsync(Guid projectId, string name, string languageCode, string version,
            bool includeDetails = true,
            CancellationToken cancellationToken = default);

        Task DeleteAsync(Guid projectId, string name, string languageCode, string version,
            CancellationToken cancellationToken = default);
    }
}