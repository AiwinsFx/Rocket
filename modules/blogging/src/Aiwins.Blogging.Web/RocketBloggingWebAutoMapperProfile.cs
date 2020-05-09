using AutoMapper;
using Aiwins.Rocket.AutoMapper;
using Aiwins.Blogging.Blogs;
using Aiwins.Blogging.Blogs.Dtos;
using Aiwins.Blogging.Pages.Admin.Blogs;
using Aiwins.Blogging.Pages.Blog.Posts;
using Aiwins.Blogging.Posts;
using EditModel = Aiwins.Blogging.Pages.Admin.Blogs.EditModel;
using IndexModel = Aiwins.Blogging.Pages.Blog.IndexModel;

namespace Aiwins.Blogging
{
    public class RocketBloggingWebAutoMapperProfile : Profile
    {
        public RocketBloggingWebAutoMapperProfile()
        {
            CreateMap<PostWithDetailsDto, EditPostViewModel>().Ignore(x=>x.Tags);
            CreateMap<NewModel.CreatePostViewModel, CreatePostDto>();
            CreateMap<CreateModel.BlogCreateModalView, CreateBlogDto>();
            CreateMap<BlogDto, EditModel.BlogEditViewModel>();
        }
    }
}
