using System;
using System.Collections.Concurrent;
using JetBrains.Annotations;

namespace Aiwins.Rocket.AspNetCore.Mvc.UI.Bundling
{
    public class RocketBundleContributorOptions
    {
        public ConcurrentDictionary<Type, BundleContributorCollection> AllExtensions { get; }

        public RocketBundleContributorOptions()
        {
            AllExtensions = new ConcurrentDictionary<Type, BundleContributorCollection>();
        }

        public BundleContributorCollection Extensions<TContributor>()
        {
            return Extensions(typeof(TContributor));
        }

        public BundleContributorCollection Extensions([NotNull] Type contributorType)
        {
            Check.NotNull(contributorType, nameof(contributorType));

            return AllExtensions.GetOrAdd(
                contributorType,
                _ => new BundleContributorCollection()
            );
        }
    }
}
