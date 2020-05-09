using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Aiwins.Rocket;
using Aiwins.Rocket.AspNetCore.Mvc;
using Aiwins.Docs.Admin.Documents;

namespace Aiwins.Docs.Admin
{
    [RemoteService]
    [Area("docs")]
    [ControllerName("DocumentsAdmin")]
    [Route("api/docs/admin/documents")]
    public class DocumentsAdminController : RocketController, IDocumentAdminAppService
    {
        private readonly IDocumentAdminAppService _documentAdminAppService;

        public DocumentsAdminController(IDocumentAdminAppService documentAdminAppService)
        {
            _documentAdminAppService = documentAdminAppService;
        }

        [HttpPost]
        [Route("ClearCache")]
        public Task ClearCacheAsync(ClearCacheInput input)
        {
            return _documentAdminAppService.ClearCacheAsync(input);
        }

        [HttpPost]
        [Route("PullAll")]
        public Task PullAllAsync(PullAllDocumentInput input)
        {
            return _documentAdminAppService.PullAllAsync(input);
        }

        [HttpPost]
        [Route("Pull")]
        public Task PullAsync(PullDocumentInput input)
        {
             return _documentAdminAppService.PullAsync(input);
        }
    }
}
