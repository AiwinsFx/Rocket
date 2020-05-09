using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Aiwins.Rocket;
using Aiwins.Rocket.AspNetCore.Mvc;
using Aiwins.Blogging.Comments;
using Aiwins.Blogging.Comments.Dtos;

namespace Aiwins.Blogging
{
    [RemoteService(Name = BloggingRemoteServiceConsts.RemoteServiceName)]
    [Area("blogging")]
    [Route("api/blogging/comments")]
    public class CommentsController : RocketController, ICommentAppService
    {
        private readonly ICommentAppService _commentAppService;

        public CommentsController(ICommentAppService commentAppService)
        {
            _commentAppService = commentAppService;
        }

        [HttpGet]
        [Route("hierarchical/{postId}")]
        public Task<List<CommentWithRepliesDto>> GetHierarchicalListOfPostAsync(Guid postId)
        {
            return _commentAppService.GetHierarchicalListOfPostAsync(postId);
        }

        [HttpPost]
        public Task<CommentWithDetailsDto> CreateAsync(CreateCommentDto input)
        {
            return _commentAppService.CreateAsync(input);
        }

        [HttpPut]
        [Route("{id}")]
        public Task<CommentWithDetailsDto> UpdateAsync(Guid id, UpdateCommentDto input)
        {
            return _commentAppService.UpdateAsync(id, input);
        }

        [HttpDelete]
        [Route("{id}")]
        public Task DeleteAsync(Guid id)
        {
            return _commentAppService.DeleteAsync(id);
        }
    }
}
