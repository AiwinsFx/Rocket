using System;
using System.Threading.Tasks;
using Aiwins.Rocket.Application.Services;

namespace Aiwins.Docs.Admin.Documents
{
    public interface IDocumentAdminAppService : IApplicationService
    {
        Task ClearCacheAsync(ClearCacheInput input);

        Task PullAllAsync(PullAllDocumentInput input);

        Task PullAsync(PullDocumentInput input);
    }
}
