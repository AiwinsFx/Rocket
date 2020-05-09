﻿using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using Aiwins.Rocket.Domain.Repositories.MongoDB;
using Aiwins.Rocket.MongoDB;
using Aiwins.Docs.MongoDB;

namespace Aiwins.Docs.Documents
{
    public class MongoDocumentRepository : MongoDbRepository<IDocsMongoDbContext, Document, Guid>, IDocumentRepository
    {
        public MongoDocumentRepository(IMongoDbContextProvider<IDocsMongoDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }

        public async Task<List<Document>> GetListByProjectId(Guid projectId, CancellationToken cancellationToken = default)
        {
            return await GetMongoQueryable().Where(d => d.ProjectId == projectId).ToListAsync(cancellationToken);
        }

        public async Task<Document> FindAsync(Guid projectId, string name, string languageCode, string version,
            bool includeDetails = true,
            CancellationToken cancellationToken = default)
        {
            return await GetMongoQueryable().FirstOrDefaultAsync(x => x.ProjectId == projectId &&
                                                                      x.Name == name &&
                                                                      x.LanguageCode == languageCode &&
                                                                      x.Version == version, cancellationToken);
        }

        public async Task DeleteAsync(Guid projectId, string name, string languageCode, string version,
            CancellationToken cancellationToken = default)
        {
            await DeleteAsync(x =>
                x.ProjectId == projectId && x.Name == name && x.LanguageCode == languageCode &&
                x.Version == version, cancellationToken: cancellationToken);
        }
    }
}