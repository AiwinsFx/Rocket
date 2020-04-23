using System.Collections.Generic;
using JetBrains.Annotations;

namespace Aiwins.Rocket.UI.Navigation
{
    public class RocketNavigationOptions
    {
        [NotNull]
        public List<IMenuContributor> MenuContributors { get; }

        public RocketNavigationOptions()
        {
            MenuContributors = new List<IMenuContributor>();
        }
    }
}