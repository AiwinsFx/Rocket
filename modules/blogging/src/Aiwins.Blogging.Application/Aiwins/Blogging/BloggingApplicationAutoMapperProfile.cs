using AutoMapper;
using Aiwins.Rocket.AutoMapper;
using Aiwins.Blogging.Blogs;
using Aiwins.Blogging.Blogs.Dtos;
using Aiwins.Blogging.Comments;
using Aiwins.Blogging.Comments.Dtos;
using Aiwins.Blogging.Posts;
using Aiwins.Blogging.Tagging;
using Aiwins.Blogging.Tagging.Dtos;
using Aiwins.Blogging.Users;

namespace Aiwins.Blogging
{
    public class BloggingApplicationAutoMapperProfile : Profile
    {
        public BloggingApplicationAutoMapperProfile()
        {
            CreateMap<Blog, BlogDto>();
            CreateMap<BlogUser, BlogUserDto>();
            CreateMap<Post, PostWithDetailsDto>().Ignore(x=>x.Writer).Ignore(x=>x.CommentCount).Ignore(x=>x.Tags);
            CreateMap<Comment, CommentWithDetailsDto>().Ignore(x => x.Writer);
            CreateMap<Tag, TagDto>();
        }
    }
}
