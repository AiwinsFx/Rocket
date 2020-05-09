using System.Collections.Generic;

namespace Aiwins.Docs.Documents
{
    public class DocumentParameterDto
    {
        public string Name { get; set; }

        public string DisplayName { get; set; }

        public Dictionary<string, string> Values { get; set; }
    }
}