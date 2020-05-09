using System.Collections.Generic;

namespace Aiwins.Docs.HtmlConverting
{
    public class DocumentPartialTemplateWithValuesDto
    {
        public string Path { get; set; }

        public Dictionary<string, string> Parameters { get; set; }
    }
}