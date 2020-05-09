using System;
using System.Collections.Generic;
using System.Text;
using Aiwins.Rocket.Domain.Services;

namespace Aiwins.Docs.GitHub.Documents
{
    public interface IGithubPatchAnalyzer : IDomainService
    {
        bool HasPatchSignificantChanges(string patch);
    }
}
