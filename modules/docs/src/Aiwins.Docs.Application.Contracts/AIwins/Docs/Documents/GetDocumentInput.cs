using System;
using System.ComponentModel.DataAnnotations;
using Aiwins.Docs.Language;
using Aiwins.Docs.Projects;

namespace Aiwins.Docs.Documents
{
    public class GetDocumentInput
    {
        public Guid ProjectId { get; set; }

        [Required]
        [StringLength(DocumentConsts.MaxNameLength)]
        public string Name { get; set; }

        [StringLength(ProjectConsts.MaxVersionNameLength)]
        public string Version { get; set; }

        [Required]
        [StringLength(LanguageConsts.MaxLanguageCodeLength)]
        public string LanguageCode { get; set; }
    }
}