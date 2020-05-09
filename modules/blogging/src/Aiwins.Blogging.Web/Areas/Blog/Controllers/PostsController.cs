using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Aiwins.Rocket.AspNetCore.Mvc;
using Aiwins.Blogging.Posts;

namespace Aiwins.Blogging.Areas.Blog.Controllers
{
    //TODO: Is that being used?

    [Area("Blog")]
    [Route("Blog/[controller]/[action]")]
    public class PostsController : BloggingControllerBase
    {
        private readonly IPostAppService _postAppService;

        public PostsController(IPostAppService postAppService)
        {
            _postAppService = postAppService;
        }

        [HttpPost]
        public async Task Delete(Guid id)
        {
            await _postAppService.DeleteAsync(id);
        }
    }
}
