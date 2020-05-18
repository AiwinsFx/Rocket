using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Aiwins.Rocket.Cli.Http;
using Aiwins.Rocket.Cli.ProjectBuilding.Templates.App;
using Aiwins.Rocket.Cli.ProjectBuilding.Templates.MvcModule;
using Aiwins.Rocket.DependencyInjection;
using Aiwins.Rocket.Http;
using Aiwins.Rocket.IO;
using Aiwins.Rocket.Json;
using Aiwins.Rocket.Threading;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;

namespace Aiwins.Rocket.Cli.ProjectBuilding {
    public class RocketIoSourceCodeStore : ISourceCodeStore, ITransientDependency {
        public ILogger<RocketIoSourceCodeStore> Logger { get; set; }

        protected RocketCliOptions Options { get; }

        protected IJsonSerializer JsonSerializer { get; }

        protected IRemoteServiceExceptionHandler RemoteServiceExceptionHandler { get; }

        protected ICancellationTokenProvider CancellationTokenProvider { get; }

        public RocketIoSourceCodeStore (
            IOptions<RocketCliOptions> options,
            IJsonSerializer jsonSerializer,
            IRemoteServiceExceptionHandler remoteServiceExceptionHandler,
            ICancellationTokenProvider cancellationTokenProvider) {
            JsonSerializer = jsonSerializer;
            RemoteServiceExceptionHandler = remoteServiceExceptionHandler;
            CancellationTokenProvider = cancellationTokenProvider;
            Options = options.Value;

            Logger = NullLogger<RocketIoSourceCodeStore>.Instance;
        }

        public async Task<TemplateFile> GetAsync (
            string name,
            string type,
            string version = null,
            string templateSource = null) {
            DirectoryHelper.CreateIfNotExists (CliPaths.TemplateCache);

            string latestVersion;

#if DEBUG
            latestVersion = await GetLatestSourceCodeVersionAsync (name, type, $"{CliUrls.WwwRocketIoProduction}api/download/{type}/get-version/");
#else
            latestVersion = await GetLatestSourceCodeVersionAsync (name, type);
#endif
            if (version == null) {
                if (latestVersion == null) {
                    Logger.LogWarning ("The remote service is currently unavailable, please specify the version.");
                    Logger.LogWarning (string.Empty);
                    Logger.LogWarning ("Find the following template in your cache directory: ");
                    Logger.LogWarning ("\t Template Name\tVersion");

                    var templateList = GetLocalTemplates ();
                    foreach (var cacheFile in templateList) {
                        Logger.LogWarning ($"\t {cacheFile.TemplateName}\t\t{cacheFile.Version}");
                    }

                    Logger.LogWarning (string.Empty);
                    throw new CliUsageException ("Use command: rocket new Acme.BookStore -v version");
                }

                version = latestVersion;
            }

            string nugetVersion;

#if DEBUG
            nugetVersion = version;
#else
            nugetVersion = (await GetTemplateNugetVersionAsync (name, type, version)) ?? version;
#endif

            if (!string.IsNullOrWhiteSpace (templateSource) && !IsNetworkSource (templateSource)) {
                Logger.LogInformation ("Using local " + type + ": " + name + ", version: " + version);
                return new TemplateFile (File.ReadAllBytes (Path.Combine (templateSource, name + "-" + version + ".zip")), version, latestVersion, nugetVersion);
            }

            var localCacheFile = Path.Combine (CliPaths.TemplateCache, name + "-" + version + ".zip");

#if DEBUG
            if (File.Exists (localCacheFile)) {
                return new TemplateFile (File.ReadAllBytes (localCacheFile), version, latestVersion, nugetVersion);
            }
#endif

            if (Options.CacheTemplates && File.Exists (localCacheFile) && templateSource.IsNullOrWhiteSpace ()) {
                Logger.LogInformation ("Using cached " + type + ": " + name + ", version: " + version);
                return new TemplateFile (File.ReadAllBytes (localCacheFile), version, latestVersion, nugetVersion);
            }

            Logger.LogInformation ("Downloading " + type + ": " + name + ", version: " + version);

            var fileContent = await DownloadSourceCodeContentAsync (
                new SourceCodeDownloadInputDto {
                    Name = name,
                        Type = type,
                        TemplateSource = templateSource,
                        Version = version
                }
            );

            if (Options.CacheTemplates && templateSource.IsNullOrWhiteSpace ()) {
                File.WriteAllBytes (localCacheFile, fileContent);
            }

            return new TemplateFile (fileContent, version, latestVersion, nugetVersion);
        }

        private async Task<string> GetLatestSourceCodeVersionAsync (string name, string type, string url = null) {
            if (url == null) {
                url = $"{CliUrls.WwwRocketIo}api/download/{type}/get-version/";
            }

            try {
                using (var client = new CliHttpClient (TimeSpan.FromMinutes (10))) {
                    var response = await client.PostAsync (
                        url,
                        new StringContent (
                            JsonSerializer.Serialize (
                                new GetLatestSourceCodeVersionDto { Name = name }
                            ),
                            Encoding.UTF8,
                            MimeTypes.Application.Json
                        ),
                        CancellationTokenProvider.Token
                    );

                    await RemoteServiceExceptionHandler.EnsureSuccessfulHttpResponseAsync (response);

                    var result = await response.Content.ReadAsStringAsync ();

                    return JsonSerializer.Deserialize<GetVersionResultDto> (result).Version;
                }
            } catch (Exception ex) {
                Console.WriteLine ("Error occured while getting the latest version from {0} : {1}", url, ex.Message);
                return null;
            }
        }

        private async Task<string> GetTemplateNugetVersionAsync (string name, string type, string version) {
            var url = $"{CliUrls.WwwRocketIo}api/download/{type}/get-nuget-version/";

            try {
                using (var client = new CliHttpClient (TimeSpan.FromMinutes (10))) {
                    var response = await client.PostAsync (
                        url,
                        new StringContent (
                            JsonSerializer.Serialize (
                                new GetTemplateNugetVersionDto { Name = name, Version = version }
                            ),
                            Encoding.UTF8,
                            MimeTypes.Application.Json
                        ),
                        CancellationTokenProvider.Token
                    );

                    await RemoteServiceExceptionHandler.EnsureSuccessfulHttpResponseAsync (response);

                    var result = await response.Content.ReadAsStringAsync ();

                    return JsonSerializer.Deserialize<GetVersionResultDto> (result).Version;
                }
            } catch (Exception ex) {
                Console.WriteLine ("Error occured while getting the template nuget version from {0} : {1}", url, ex.Message);
                return null;
            }
        }

        private async Task<byte[]> DownloadSourceCodeContentAsync (SourceCodeDownloadInputDto input) {
            var url = $"{CliUrls.WwwRocketIo}api/download/{input.Type}/";

            try {
                using (var client = new CliHttpClient (TimeSpan.FromMinutes (10))) {
                    HttpResponseMessage responseMessage;

                    if (input.TemplateSource.IsNullOrWhiteSpace ()) {
                        responseMessage = await client.PostAsync (
                            url,
                            new StringContent (JsonSerializer.Serialize (input), Encoding.UTF8, MimeTypes.Application.Json),
                            CancellationTokenProvider.Token
                        );
                    } else {
                        responseMessage = await client.GetAsync (input.TemplateSource, CancellationTokenProvider.Token);
                    }

                    await RemoteServiceExceptionHandler.EnsureSuccessfulHttpResponseAsync (responseMessage);

                    return await responseMessage.Content.ReadAsByteArrayAsync ();
                }
            } catch (Exception ex) {
                Console.WriteLine ("Error occured while downloading source-code from {0} : {1}", url, ex.Message);
                throw;
            }
        }

        private bool IsNetworkSource (string source) {
            return source.ToLower ().StartsWith ("http");
        }

        private List < (string TemplateName, string Version) > GetLocalTemplates () {
            var templateList = new List < (string TemplateName, string Version) > ();

            var stringBuilder = new StringBuilder ();
            foreach (var cacheFile in Directory.GetFiles (CliPaths.TemplateCache)) {
                stringBuilder.AppendLine (cacheFile);
            }

            var matches = Regex.Matches (stringBuilder.ToString (), $"({AppTemplate.TemplateName}|{AppProTemplate.TemplateName}|{ModuleTemplate.TemplateName}|{ModuleProTemplate.TemplateName})-(.+).zip");
            foreach (Match match in matches) {
                templateList.Add ((match.Groups[1].Value, match.Groups[2].Value));
            }

            return templateList;
        }

        public class SourceCodeDownloadInputDto {
            public string Name { get; set; }

            public string Version { get; set; }

            public string Type { get; set; }

            public string TemplateSource { get; set; }
        }

        public class GetLatestSourceCodeVersionDto {
            public string Name { get; set; }
        }

        public class GetTemplateNugetVersionDto {
            public string Name { get; set; }

            public string Version { get; set; }
        }

        public class GetVersionResultDto {
            public string Version { get; set; }
        }
    }
}