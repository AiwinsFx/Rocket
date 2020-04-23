using System.Collections.Generic;
using JetBrains.Annotations;

namespace Aiwins.Rocket.AspNetCore.Mvc.UI.Theme.Shared.Toolbars
{
    public class RocketToolbarOptions
    {
        [NotNull]
        public List<IToolbarContributor> Contributors { get; }

        public RocketToolbarOptions()
        {
            Contributors = new List<IToolbarContributor>();
        }
    }
}
