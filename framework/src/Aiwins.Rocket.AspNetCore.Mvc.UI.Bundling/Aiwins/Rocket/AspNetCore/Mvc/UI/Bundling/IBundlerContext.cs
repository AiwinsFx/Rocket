using System.Collections.Generic;

namespace Aiwins.Rocket.AspNetCore.Mvc.UI.Bundling
{
    public interface IBundlerContext
    {
        string BundleRelativePath { get; }

        IReadOnlyList<string> ContentFiles { get; }

        bool IsMinificationEnabled { get; }
    }
}