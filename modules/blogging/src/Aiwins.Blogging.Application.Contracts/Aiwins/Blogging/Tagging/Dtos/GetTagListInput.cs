using System;
using System.Collections.Generic;

namespace Aiwins.Blogging.Tagging.Dtos
{
    public class GetTagListInput
    {
        public IEnumerable<Guid> Ids { get; set; }
    }
}