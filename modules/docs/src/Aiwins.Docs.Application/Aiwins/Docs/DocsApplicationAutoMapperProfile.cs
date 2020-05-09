using AutoMapper;
using Aiwins.Docs.Documents;
using Aiwins.Docs.Projects;
using Aiwins.Rocket.AutoMapper;

namespace Aiwins.Docs
{
    public class DocsApplicationAutoMapperProfile : Profile
    {
        public DocsApplicationAutoMapperProfile()
        {
            CreateMap<Project, ProjectDto>();
            CreateMap<VersionInfo, VersionInfoDto>();
            CreateMap<Document, DocumentWithDetailsDto>()
                .Ignore(x => x.Project).Ignore(x => x.Contributors);
            CreateMap<DocumentContributor, DocumentContributorDto>();
            CreateMap<DocumentResource, DocumentResourceDto>();
        }
    }
}