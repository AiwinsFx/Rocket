using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Aiwins.Docs.GitHub.Documents;
using Aiwins.Docs.Projects;
using Aiwins.Rocket.Domain.Services;

namespace Aiwins.Docs.Documents {
    public interface IDocumentSource : IDomainService {
        Task<Document> GetDocumentAsync (Project project, string documentName, string languageCode, string version, DateTime? lastKnownSignificantUpdateTime = null);

        Task<List<VersionInfo>> GetVersionsAsync (Project project);

        Task<DocumentResource> GetResource (Project project, string resourceName, string languageCode, string version);

        Task<LanguageConfig> GetLanguageListAsync (Project project, string version);
    }
}