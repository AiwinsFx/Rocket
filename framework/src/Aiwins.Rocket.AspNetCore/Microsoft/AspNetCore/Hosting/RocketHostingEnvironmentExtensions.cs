using System;
using Microsoft.Extensions.Configuration;

namespace Microsoft.AspNetCore.Hosting {
    public static class RocketHostingEnvironmentExtensions {
        public static IConfigurationRoot BuildConfiguration (
            this IWebHostEnvironment env,
            RocketConfigurationBuilderOptions options = null) {
            options = options ?? new RocketConfigurationBuilderOptions ();

            if (options.BasePath.IsNullOrEmpty ()) {
                options.BasePath = env.ContentRootPath;
            }

            if (options.EnvironmentName.IsNullOrEmpty ()) {
                options.EnvironmentName = env.EnvironmentName;
            }

            return ConfigurationHelper.BuildConfiguration (options);
        }
    }
}