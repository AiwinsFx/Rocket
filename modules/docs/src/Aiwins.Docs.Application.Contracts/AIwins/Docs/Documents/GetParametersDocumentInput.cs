using System;
using System.ComponentModel.DataAnnotations;
using Aiwins.Docs.Language;
using Aiwins.Docs.Projects;

namespace Aiwins.Docs.Documents
{
    public class GetParametersDocumentInput
    {
        public Guid ProjectId { get; set; }

        [StringLength(ProjectConsts.MaxVersionNameLength)]
        public string Version { get; set; }

        [Required]
        [StringLength(LanguageConsts.MaxLanguageCodeLength)]
        public string LanguageCode { get; set; }
    }
}