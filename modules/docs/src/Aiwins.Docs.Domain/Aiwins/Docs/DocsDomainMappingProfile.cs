using AutoMapper;
using Aiwins.Docs.Documents;
using Aiwins.Docs.Projects;

namespace Aiwins.Docs
{
    public class DocsDomainMappingProfile : Profile
    {
        public DocsDomainMappingProfile()
        {
            CreateMap<Document, DocumentEto>();
            CreateMap<Project, ProjectEto>();
        }
    }
}