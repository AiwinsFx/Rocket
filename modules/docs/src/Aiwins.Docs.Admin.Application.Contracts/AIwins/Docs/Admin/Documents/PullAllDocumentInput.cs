using System;
using System.ComponentModel.DataAnnotations;
using Aiwins.Docs.Documents;

namespace Aiwins.Docs.Admin.Documents
{
    public class PullAllDocumentInput
    {
        public Guid ProjectId { get; set; }

        [StringLength(DocumentConsts.MaxLanguageCodeNameLength)]
        public string LanguageCode { get; set; }

        [StringLength(DocumentConsts.MaxVersionNameLength)]
        public string Version { get; set; }
    }
}