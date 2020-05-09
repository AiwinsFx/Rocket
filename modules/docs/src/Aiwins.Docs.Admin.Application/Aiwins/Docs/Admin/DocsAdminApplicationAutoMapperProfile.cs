using AutoMapper;
using Aiwins.Docs.Admin.Projects;
using Aiwins.Docs.Projects;

namespace Aiwins.Docs.Admin
{
    public class DocsAdminApplicationAutoMapperProfile : Profile
    {
        public DocsAdminApplicationAutoMapperProfile()
        {
            CreateMap<Project, ProjectDto>();
        }
    }
}