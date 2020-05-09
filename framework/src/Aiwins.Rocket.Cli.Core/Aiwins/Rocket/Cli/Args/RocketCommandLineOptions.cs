using System;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace Aiwins.Rocket.Cli.Args
{
    public class RocketCommandLineOptions : Dictionary<string, string>
    {
        [CanBeNull]
        public string GetOrNull([NotNull] string name, params string[] alternativeNames)
        {
            Check.NotNullOrWhiteSpace(name, nameof(name));

            var value = this.GetOrDefault(name);
            if (!value.IsNullOrWhiteSpace())
            {
                return value;
            }

            if (!alternativeNames.IsNullOrEmpty())
            {
                foreach (var alternativeName in alternativeNames)
                {
                    value = this.GetOrDefault(alternativeName);
                    if (!value.IsNullOrWhiteSpace())
                    {
                        return value;
                    }
                }
            }

            return null;
        }
    }
}