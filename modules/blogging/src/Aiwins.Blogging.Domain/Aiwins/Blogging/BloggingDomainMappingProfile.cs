using AutoMapper;
using Aiwins.Blogging.Blogs;
using Aiwins.Blogging.Comments;
using Aiwins.Blogging.Posts;
using Aiwins.Blogging.Tagging;

namespace Aiwins.Blogging
{
    public class BloggingDomainMappingProfile : Profile
    {
        public BloggingDomainMappingProfile()
        {
            CreateMap<Blog, BlogEto>();
            CreateMap<Comment, CommentEto>();
            CreateMap<Post, PostEto>();
            CreateMap<Tag, TagEto>();
        }
    }
}